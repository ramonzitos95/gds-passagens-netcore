using AutoMapper;
using cliqx.gds.contract.Dtos;
using cliqx.gds.contract.GdsModels;
using cliqx.gds.contract.GdsModels.Query;
using cliqx.gds.contract.GdsModels.RequestDtos;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.Dto;
using cliqx.gds.contract.Models.Identity;
using cliqx.gds.contract.Models.PluginConfigurations;

namespace ProSales.API.Helpers;


public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Plugin, CreatePlugin>().ReverseMap();
        CreateMap<Client, CreateClientDto>().ReverseMap();
        CreateMap<Trip, SeatQuery>().ReverseMap();
        CreateMap<Client, ClientDto>().ReverseMap();
        //CreateMap<Order, CreatePreOrder>().ReverseMap();
        //CreateMap<OrderBase, OrderBaseDto>().ReverseMap();
        CreateMap<Trip, TripDto>().ReverseMap();
        CreateMap<Seat, SeatDto>().ReverseMap();
        CreateMap<CreatePreOrderDto, PreOrder>().ReverseMap();
        CreateMap<PreOrderItemDto, PreOrderItem>().ReverseMap();
        CreateMap<CreateUser, User>().ReverseMap();
    }

}
