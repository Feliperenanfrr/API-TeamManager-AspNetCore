using AutoMapper;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Model;

namespace TeamManager.Application.Mapping;

public class FinancialTransactionProfile : Profile
{
    public FinancialTransactionProfile()
    {
        CreateMap<FinancialTransactionCreateDto, FinancialTransaction>();

        CreateMap<FinancialTransactionUpdateDto, FinancialTransaction>();

        CreateMap<FinancialTransaction, FinancialTransactionResponseDto>()
            .ForMember(
                dest => dest.TypeTransaction,
                opt
                    => opt.MapFrom(src => src.TypeTransaction ? "Receita" : "Despesa")
            );
    }
}