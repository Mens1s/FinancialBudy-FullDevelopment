using AutoMapper;
using FinancialBuddy.Application.DTOs.Goal;
using FinancialBuddy.Application.DTOs.Subscription;
using FinancialBuddy.Application.DTOs.Transaction;
using FinancialBuddy.Application.DTOs.Transfer;
using FinancialBuddy.Application.DTOs.User;
using FinancialBuddy.Domain.Entities;


namespace FinancialBuddy.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserRequest, User>();
            CreateMap<UpdateUserRequest, User>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<CreateTransactionRequest, Transaction>();
            CreateMap<Transfer, TransferDto>();
            CreateMap<CreateTransferRequest, Transfer>();
            CreateMap<Subscription, SubscriptionDto>();
            CreateMap<CreateSubscriptionRequest, Subscription>();
            CreateMap<Goal, GoalDto>();
            CreateMap<CreateGoalRequest, Goal>();
        }
    }
}
