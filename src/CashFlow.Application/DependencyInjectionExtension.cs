namespace CashFlow.Application;

using CashFlow.Application.UseCases.Expenses.Register;
using Microsoft.Extensions.DependencyInjection;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services) 
    {
        services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
    }
}
