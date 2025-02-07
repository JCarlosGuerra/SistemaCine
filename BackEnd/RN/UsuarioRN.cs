﻿using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs.Categoria;
using BackEnd.DTOs;
using BackEnd.Helpers;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using BackEnd.DTOs.Usuario;

namespace BackEnd.RN
{
    public class UsuarioRN
    {
        private readonly CineContext context;
        private readonly IMapper mapper;

        public UsuarioRN(CineContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ResponseListDTO<UsuarioDTO>> getAll(PaginacionDTO paginacion)
        {
            var query = context.Usuarios.AsQueryable();

            var datosPaginacion = await query.datosPaginacion(paginacion.CantidadRegistrosPorPagina);
            var entidades = await query.Paginar(paginacion).ToListAsync();

            var list = mapper.Map<List<UsuarioDTO>>(entidades);

            return new ResponseListDTO<UsuarioDTO>
            {
                quanty = int.Parse(datosPaginacion["cantidadPaginas"]),
                page = paginacion.Pagina,
                total = int.Parse(datosPaginacion["totalRegistros"]),
                value = list
            };
        }

        public async Task<ResponseDTO<UsuarioDTO>> getById(int id)
        {

            var entity = await context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("No existe el recurso");

            var usuarioDTO = mapper.Map<UsuarioDTO>(entity);


            return new ResponseDTO<UsuarioDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = usuarioDTO,
            };


        }
        public async Task<ResponseDTO<UsuarioInsertDTO>> postInsert(UsuarioInsertDTO usuarioInsertDTO)
        {

            var entity = mapper.Map<Usuario>(usuarioInsertDTO);

            context.Usuarios.Add(entity);

            await context.SaveChangesAsync();

            if (entity == null)
                throw new Exception("No existe el recurso");

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<UsuarioInsertDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = usuarioInsertDTO,
            };

        }
        public async Task<ResponseDTO<UsuarioDTO>> putUpdate(int id, UsuarioUpdateDTO usuarioUpdateDTO)
        {

            var entity = await context.Usuarios.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            entity = mapper.Map(usuarioUpdateDTO, entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var usuarioUpdate = mapper.Map<UsuarioDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<UsuarioDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = usuarioUpdate,
            };

        }
        public async Task<ResponseDTO<UsuarioDTO>> delete(int id)
        {

            var entity = await context.Usuarios.FindAsync(id);

            if (entity == null)
                throw new Exception("El Registro para actualizar no existe");

            context.Usuarios.Remove(entity);

            // context.Entry(autor).State = EntityState.Modified;
            await context.SaveChangesAsync();

            var usuario = mapper.Map<UsuarioDTO>(entity);

            //var categoriaDTO = mapper.Map<CategoriaDTO>(entity);
            return new ResponseDTO<UsuarioDTO>
            {
                Success = true,
                StatusCode = 200,
                Message = "OK",
                value = usuario,
            };

        }
    }
}
