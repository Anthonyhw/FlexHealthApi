﻿using AutoMapper;
using FlexHealthDomain.DTOs;
using FlexHealthDomain.Models;
using FlexHealthDomain.Repositories;
using FlexHealthDomain.Services;
using FlexHealthInfrastructure.Repositories;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthInfrastructure.Services
{
    public class NewService : INewService
    {

        private readonly INewRepository _newRepository;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _environment;
        public NewService(INewRepository newRepository, IMapper mapper, IHostingEnvironment environment)
        {
            _newRepository = newRepository;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<IEnumerable<Noticia>> GetNews()
        {
            try
            {
                var newRequest = await _newRepository.GetNews();
                if (newRequest != null)
                {
                    return newRequest;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Noticia> GetNewById(int id)
        {
            try
            {
                var newRequest = await _newRepository.GetNewById(id);
                if (newRequest != null)
                {
                    return newRequest;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CreateNew(NoticiaDto createNew)
        {
            try
            {
                var mapNew = _mapper.Map<Noticia>(createNew);
                Random rand = new Random();
                
                mapNew.ImagemUrl = mapNew.ImagemUrl + DateTime.Now.ToShortDateString() + "/" + rand.Next(10000, 100000);
                mapNew.ImagemUrl = mapNew.ImagemUrl.Replace("/", "-");
                var newResult = _newRepository.CreateNew(mapNew);
                if (newResult != null)
                {
                    string uploadFolder = Path.Combine(_environment.ContentRootPath + @"Resources\NewsImages\" + mapNew.ImagemUrl + Path.GetExtension(createNew.Imagem.FileName));
                    using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                    {
                        createNew.Imagem.CopyTo(fileStream);
                    }

                    return newResult;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
