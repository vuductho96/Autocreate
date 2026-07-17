using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OvertimeScheduler.Models;

namespace OvertimeScheduler.Services
{
    public class ZaloBotService
    {
        private ChromeDriver _driver;
        private string _targetChatName = "";
        private bool _isListening = false;
        private CancellationTokenSource _cts;

        public event Action<string, string> OnLogMessage;
        public event Action<(Employee employee, DateTime date)> OnLeaveRequestReceived;

        public bool IsRunning => _driver != null;

        public void StartBot(string targetChatName)
        {
            if (_driver != null) return;
            _targetChatName = targetChatName;

            Log("Starting Zalo Bot (Chrome browser)...");

            var options = new ChromeOptions();
            // Thiết lập lưu profile Chrome để giữ lại đăng nhập Zalo
            string profilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ZaloChromeProfile");
            options.AddArgument($"--user-data-dir={profilePath}");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--start-maximized");

            try
            {
                _driver = new ChromeDriver(options);
                _driver.Navigate().GoToUrl("https://chat.zalo.me/");
                
                Log("Vui lòng quét mã QR để đăng nhập Zalo Web (nếu ứng dụng yêu cầu).");

                _isListening = true;
                _cts = new CancellationTokenSource();
                
                // Chạy ngầm luồng lắng nghe tin nhắn Zalo
                Task.Run(() => StartListeningLoop(_cts.Token));
            }
            catch (Exception ex)
            {
                Log($"Lỗi khởi động Chrome: {ex.Message}");
                StopBot();
            }
        }

        public void StopBot()
        {
            _isListening = false;
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }

            if (_driver != null)
            {
                try
                {
                    _driver.Quit();
                }
                catch { }
                _driver = null;
            }
            Log("Zalo Bot đã dừng.");
        }

        public void SendExcelFile(string filePath)
        {
            if (_driver == null || string.IsNullOrEmpty(_targetChatName)) return;

            Task.Run(() =>
            {
                try
                {
                    Log($"Đang gửi file Excel tới '{_targetChatName}'...");
                    if (!OpenChat(_targetChatName))
                    {
                        Log($"Không thể mở hội thoại chat với '{_targetChatName}'");
                        return;
                    }

                    // Định vị nút tải file ẩn trong Zalo Web và truyền đường dẫn file vào
                    var fileInput = _driver.FindElement(By.XPath("//input[@type='file']"));
                    fileInput.SendKeys(filePath);
                    
                    Thread.Sleep(2000); // Đợi upload file
                    
                    // Gửi tin nhắn thông báo đính kèm
                    SendTextMessage("Hệ thống đã tự động cập nhật lại lịch tăng ca tuần này!");
                    Log("Đã gửi file Excel thành công.");
                }
                catch (Exception ex)
                {
                    Log($"Lỗi gửi file: {ex.Message}");
                }
            });
        }

        private bool OpenChat(string targetName)
        {
            try
            {
                // Định vị ô tìm kiếm Zalo
                var searchInput = _driver.FindElement(By.XPath("//input[@id='contact-search-input' or @placeholder='Tìm kiếm' or contains(@placeholder, 'Tìm kiếm')]"));
                searchInput.Clear();
                searchInput.SendKeys(targetName);
                Thread.Sleep(1500);

                // Nhấn Enter để mở hội thoại đầu tiên tìm thấy
                searchInput.SendKeys(OpenQA.Selenium.Keys.Enter);
                Thread.Sleep(1000);
                return true;
            }
            catch (Exception ex)
            {
                Log($"Lỗi mở khung chat: {ex.Message}");
                return false;
            }
        }

        private void SendTextMessage(string message)
        {
            try
            {
                var chatInput = _driver.FindElement(By.XPath("//div[@id='rich-input']"));
                chatInput.SendKeys(message);
                chatInput.SendKeys(OpenQA.Selenium.Keys.Enter);
            }
            catch (Exception ex)
            {
                Log($"Lỗi gửi tin nhắn: {ex.Message}");
            }
        }

        private async Task StartListeningLoop(CancellationToken token)
        {
            string lastProcessedText = "";

            while (_isListening && !token.IsCancellationRequested)
            {
                try
                {
                    if (_driver != null)
                    {
                        // Lấy các phần tử tin nhắn trong khung chat
                        var messageElements = _driver.FindElements(By.XPath("//div[contains(@class,'card--text') or contains(@class,'msg-card') or contains(@class,'message-text') or contains(@class,'card')]"));
                        if (messageElements.Count > 0)
                        {
                            var lastElement = messageElements.Last();
                            string text = lastElement.Text;

                            if (!string.IsNullOrEmpty(text) && text != lastProcessedText)
                            {
                                lastProcessedText = text;
                                Log($"Tin nhắn Zalo mới phát hiện: '{text}'");

                                // Parse tin nhắn nghỉ phép
                                var employees = Employee.GetMockEmployees();
                                // Sử dụng ngày đầu tuần là ngày Thứ 2 tuần hiện tại
                                DateTime today = DateTime.Today;
                                int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
                                DateTime weekStart = today.AddDays(-1 * diff).Date;

                                var result = MessageParser.ParseLeaveMessage(text, employees, weekStart);
                                if (result != null)
                                {
                                    Log($"Lệnh nghỉ phép hợp lệ: Nhân viên {result.Value.employee.Name} xin nghỉ ngày {result.Value.date:dd/MM/yyyy}");
                                    OnLeaveRequestReceived?.Invoke(result.Value);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // Bỏ qua các lỗi DOM tạm thời khi Zalo đang tải trang
                }

                await Task.Delay(10000, token); // Poll mỗi 10 giây
            }
        }

        private void Log(string message)
        {
            OnLogMessage?.Invoke(DateTime.Now.ToString("HH:mm:ss"), message);
        }
    }
}
