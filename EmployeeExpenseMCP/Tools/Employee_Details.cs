using ModelContextProtocol.Server;
using System.ComponentModel;

namespace EmployeeExpenseMCP.Tools
{
    [McpServerToolType]
    public static class Employee_Details
    {
        [McpServerTool(Name = "GetEmployeeDetails")]
        [Description("Get Details of an Employee")]
        public static string GetEmployeeDetails([Description("Employee ID")] string employeeId)
        {
            // For demonstration purposes, return a simple message.
            // In a real application, you would call an employee API to get actual employee data.
            return $"Employee details for ID {employeeId} are as follows: Name: John Doe, Position: Software Engineer.";
        }

        [McpServerTool(Name = "GetEmployeeExpenses")]
        [Description("Get Expense Details of an Employee")]
        public static string GetEmployeeExpenses([Description("Employee ID")] string employeeId)
        {
            // For demonstration purposes, return a simple message.
            // In a real application, you would call an expense API to get actual expense data.
            return $"Expense details for ID {employeeId} are as follows: Total Expenses: $1,200,000, Approved Expenses: $1,000,000.";
        }

        [McpServerTool(Name ="AllEmployeeDetails")]
        [Description("Get All Employee Details")]
        public static List<EmployeeDetails> AllEmployeeDetails()
        {
            // For demonstration purposes, return a simple list.
            // In a real application, you would call an employee API to get actual employee data.
            return
            [
                new() { EmployeeId = "E001", Name = "John Doe", Position = "Software Engineer", TotalExpenses = 1200000m, ApprovedExpenses = 1000000m },
                new() { EmployeeId = "E002", Name = "Jane Smith", Position = "Project Manager", TotalExpenses = 1500000m, ApprovedExpenses = 1300000m }
            ];
        }
    }

    public class EmployeeDetails
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
