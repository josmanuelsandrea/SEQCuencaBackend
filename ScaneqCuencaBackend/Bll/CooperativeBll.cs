﻿using AutoMapper;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

namespace ScaneqCuencaBackend.Bll
{
    public class CooperativeBll
    {
        private readonly SeqcuencabackendContext _context;
        private readonly CooperativeRepository _cooperativeR;
        private readonly IMapper _mapper;
        public CooperativeBll(SeqcuencabackendContext db, IMapper mapper)
        {
            _context = db;
            _cooperativeR = new(db);
            _mapper = mapper;
        }

        public List<CooperativeResponseModel> GetAllCooperatives()
        {
            var result = _cooperativeR.GetAll();
            List<CooperativeResponseModel> response = _mapper.Map<List<CooperativeResponseModel>>(result); 
            return response;
        }

        public CooperativeResponseModel GetCooperativeByID(int id)
        {
            var result = _cooperativeR.GetById(id);
            CooperativeResponseModel response = _mapper.Map<CooperativeResponseModel>(result);
            return response;
        }
    }
}