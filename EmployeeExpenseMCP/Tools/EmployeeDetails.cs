using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace EmployeeExpenseMCP.Tools
{
    [McpServerToolType]
    public class EmployeeDetails
    {
        private readonly ILogger<EmployeeDetails> _logger;
        private readonly HttpClient _httpClient;
        public EmployeeDetails(ILogger<EmployeeDetails> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }


        [McpServerTool(Name = "GetEmployeeDetails")]
        [Description("Get Details of an Employee")]
        public async Task<string> GetEmployeeDetailsAsync([Description("Employee ID")] string employeeId)
        {
            _logger.LogInformation($"Fetching details for Employee ID: {employeeId}");
            var data = await _httpClient.GetStringAsync($"api/employees/{employeeId}");
            return await Task.FromResult($"Employee details for ID {employeeId} are as follows: Name: John Doe, Position: Software Engineer.");
        }

        [McpServerTool(Name = "GetEmployeeExpenses")]
        [Description("Get Expense Details of an Employee")]
        public async Task<string> GetEmployeeExpensesAsync([Description("Employee ID")] string employeeId)
        {
            _logger.LogInformation($"Fetching expense details for Employee ID: {employeeId}");
            return await Task.FromResult($"Expense details for ID {employeeId} are as follows: Total Expenses: $1,200,000, Approved Expenses: $1,000,000.");
        }

        [McpServerTool(Name = "AllEmployeeDetails")]
        [Description("Get All Employee Details")]
        public async Task<List<Employee>> AllEmployeeDetailsAsync()
        {
            _logger.LogInformation("Fetching all employee details");
            var data = await _httpClient.GetStringAsync("api/employees");
            Console.WriteLine($"Fetched data: {data}");
            return null;
        }
    }

    public class Employee
    {
        [Description("Employee ID")]
        public string EmployeeId { get; set; }
        [Description("Employee Name")]
        public string Name { get; set; }
        [Description("Employee Position")]
        public string Position { get; set; }
        [Description("Total Expenses")]
        public decimal TotalExpenses { get; set; }
        [Description("Approved Expenses")]
        public decimal ApprovedExpenses { get; set; }
    }
}
