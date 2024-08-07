﻿using AutoMapper;
using HouseBuyingOrRenting.Domain;
using HouseBuyingOrRenting.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class BoardingHouseService : BaseService<BoardingHouse, BoardingHouseDto, BoardingHouseCreateDto, BoardingHouseUpdateDto>, IBoardingHouseService
    {
        private readonly IRealEstateRepository _realEstateRepository;

        private readonly IBoardingHouseRepository _boardingHouseRepository;

        private readonly IImageUrlService _imageUrlService;

        private readonly IMapper _mapper;

        private readonly MyDbContext _db;

        public BoardingHouseService(
            IBoardingHouseRepository boardingHouseRepository,
            IRealEstateRepository realEstateRepository,
            IImageUrlService imageUrlService,
            IMapper mapper,
            MyDbContext db
            ) : base(boardingHouseRepository)
        {
            _realEstateRepository = realEstateRepository;
            _boardingHouseRepository = boardingHouseRepository;
            _imageUrlService = imageUrlService;
            _mapper = mapper;
            _db = db;
        }

        public async Task<BoardingHouseDto> GetByRealEstateId(Guid realEstateId)
        {
            var boardingHouse = await _boardingHouseRepository.GetByRealEstateId(realEstateId);
            var boardingHouseDto = await MapEntityToEntityDto(boardingHouse);

            return boardingHouseDto;
        }

        public async override Task<Guid> InsertAsync(BoardingHouseCreateDto entityCreateDto)
        {

            var realEstate = _mapper.Map<RealEstate>(entityCreateDto.RealEstateCreateDto);
            var realEstateId = Guid.NewGuid();
            realEstate.CreatedDate = DateTime.Now;
            realEstate.CreatedName = "";
            realEstate.Id = realEstateId;

            var boardingHouse = await MapEntityCreateDtoToEntity(entityCreateDto);
            boardingHouse.Id = Guid.NewGuid();
            boardingHouse.RealEstateId = realEstateId;

            var imageUrlsCreateDto = entityCreateDto.RealEstateCreateDto.ImageUrlsCreateDto.Select(imageUrlCreateDto =>
            {
                imageUrlCreateDto.RealEstateId = realEstateId;
                return imageUrlCreateDto;
            }).ToList();

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    await _realEstateRepository.InsertAsync(realEstate);
                    await _imageUrlService.InsertMultiAsync(imageUrlsCreateDto);
                    var result = await BaseRepository.InsertAsync(boardingHouse);
                    transaction.Commit();
                    return realEstate.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async override Task<BoardingHouse> MapEntityCreateDtoToEntity(BoardingHouseCreateDto entityCreateDto)
        {
            var boardingHouse = _mapper.Map<BoardingHouse>(entityCreateDto);
            return boardingHouse;
        }

        public async override Task<BoardingHouseDto> MapEntityToEntityDto(BoardingHouse entity)
        {
            var boardingHouseDto = _mapper.Map<BoardingHouseDto>(entity);
            boardingHouseDto.RealEstateDto = _mapper.Map<RealEstateDto>(entity.RealEstate);
            return boardingHouseDto;
        }

        public async override Task<BoardingHouse> MapEntityUpdateDtoToEntity(Guid id, BoardingHouseUpdateDto entityUpdateDto)
        {
            var boardingHouse = _mapper.Map<BoardingHouse>(entityUpdateDto);
            boardingHouse.Id = id;
            return boardingHouse;
        }
    }
}
