using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portfolio_EF.Models
{
    public class CoinService : ICoinService
    {
        IMapper mapperDTOToEntity;
        IMapper mapperEntityToDTO;


        public CoinService()
        {
            //dataBase = new EntityUnitOfWork(name);

            mapperDTOToEntity = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CoinDTO0, Coin>();
                cfg.CreateMap<TransactionDTO0, Transaction>();
            }).CreateMapper();

            mapperEntityToDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Coin, CoinDTO0>();
                cfg.CreateMap<Transaction, TransactionDTO0>();
            }).CreateMapper();


        }

        public ObservableCollection<Coin> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
