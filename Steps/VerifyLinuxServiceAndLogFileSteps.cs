using FluentAssertions;
using Renci.SshNet;
using TechTalk.SpecFlow;

namespace YourNamespace
{
    [Binding]
    public class VerifyLinuxServiceAndLogFileSteps
    {
        private SshClient _sshClient;
        private bool _isServiceRunning;
        private bool _isLogFileExist;

        [Given(@"I connect to the Linux VM using SSH")]
        public void ConnectToLinuxVM()
        {
            _sshClient = new SshClient("your-linux-vm-hostname", "username", "password");
            _sshClient.Connect();
        }

        [When(@"I check if ""(.*)"" is running")]
        public void CheckServiceStatus(string serviceName)
        {
            var commandResult = _sshClient.RunCommand($"systemctl status {serviceName}");
            _isServiceRunning = commandResult.ExitStatus == 0;
        }

        [And(@"I verify the existence of log file ""(.*)""")]
        public void VerifyLogFileExistence(string logFilePath)
        {
            var commandResult = _sshClient.RunCommand($"ls {logFilePath}");
            _isLogFileExist = commandResult.ExitStatus == 0;
        }

        [Then(@"the service should be running and the log file should exist")]
        public void VerifyServiceAndLogFileExistence()
        {
            _isServiceRunning.Should().BeTrue("The service should be running");
            _isLogFileExist.Should().BeTrue("The log file should exist");
        }

       [AfterScenario]
        public void Cleanup()
        {
            try
            {
                if (_sshClient != null && _sshClient.IsConnected)
                    _sshClient.Disconnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred during cleanup: {ex.Message}");
            }
            finally
            {
                _sshClient?.Dispose();
            }
        }
    }
}