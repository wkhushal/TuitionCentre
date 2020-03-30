using AspNetCoreWebApi.DTOs.Upsert;
using AspNetCoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.DTOs.Mapper
{
    public static class MapFromUpsertDto
    {
        public static Course FromDto(this CourseDto dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return new Course {
                CourseId = dto.CourseId,
                Name = dto.Name,
                TuitionAgencyId = dto.TuitionAgencyId,
                //Subjects = dto.Subjects.Select(); 
            };
        }
    }
}
