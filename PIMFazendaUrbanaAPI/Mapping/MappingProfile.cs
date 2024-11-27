using AutoMapper;
using PIMFazendaUrbanaLib;
using PIMFazendaUrbanaAPI.DTOs;

namespace PIMFazendaUrbanaAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
            CreateMap<Telefone, TelefoneDTO>().ReverseMap();
            CreateMap<EnderecoViaCepDTO, EnderecoDTO>()
            .ForMember(dest => dest.CEP, opt => opt.MapFrom(src => src.cep))
            .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.logradouro))
            .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => src.complemento))
            .ForMember(dest => dest.Bairro, opt => opt.MapFrom(src => src.bairro))
            .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.localidade))
            .ForMember(dest => dest.UF, opt => opt.MapFrom(src => src.uf));
            CreateMap<Funcionario, FuncionarioDTO>().ReverseMap();
            CreateMap<Funcionario, FuncionarioSessionDTO>().ReverseMap();
            CreateMap<Fornecedor, FornecedorDTO>().ReverseMap();
            CreateMap<Cultivo, CultivoDTO>().ReverseMap();
            CreateMap<Insumo, InsumoDTO>().ReverseMap();
            CreateMap<SaidaInsumo, SaidaInsumoDTO>().ReverseMap();
            CreateMap<Producao, ProducaoDTO>().ReverseMap();
            CreateMap<EstoqueProduto, EstoqueProdutoDTO>().ReverseMap();
            CreateMap<PedidoCompra, PedidoCompraDTO>().ReverseMap();
            CreateMap<PedidoCompraItem, PedidoCompraItemDTO>().ReverseMap();
            CreateMap<PedidoVenda, PedidoVendaDTO>().ReverseMap();
            CreateMap<PedidoVendaItem, PedidoVendaItemDTO>().ReverseMap();
        }
    }
}
