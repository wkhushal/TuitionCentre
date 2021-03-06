﻿using AspNetCoreWebApi.DTOs.Query;
using AspNetCoreWebApi.Models;
using System;
using System.Linq;

namespace AspNetCoreWebApi.DTOs.Mapper
{
    public static class MapToQueryDto
    {
        public static SubjectQueryDto ToQueryDto(this Subject value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new SubjectQueryDto(value.SubjectId, value.Name, value.CreditHours, value.CourseId);
        }

        public static TeacherQueryDto ToQueryDto(this Teacher value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new TeacherQueryDto(value.TeacherId, value.TeacherType, value.PersonId);
        }

        public static StudentQueryDto ToQueryDto(this Student value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var dto = new StudentQueryDto
            {
                StudentId = value.StudentId,
            };
            
            if (!(value.Person is null))
            {
                dto.Person = new PersonQueryDto
                {
                    PersonId = value.Person.PersonId,
                    Name = value.Person.Name,
                    BirthDate = value.Person.BirthDate
                };
            }
            
            if (!(value.TuitionAgency is null))
            {
                dto.TuitionAgencyId = value.TuitionAgency?.TuitionAgencyId ?? throw new Exception("TuitionAgency member null");
            }

            if (!(value.CoursesRegistered is null))
            {
                dto.CoursesRegistered = value.CoursesRegistered?.Select(course => course.CourseId).ToList();
            }
            return dto;
        }

        public static CourseQueryDto ToQueryDto(this Course value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            return new CourseQueryDto(value.CourseId, value.Name, value.TuitionAgencyId, value.Subjects);
        }

        public static TuitionAgencyBranchQueryDto ToQueryDto(this TuitionAgencyBranch branch)
        {
            if (branch is null)
            {
                throw new ArgumentNullException(nameof(branch));
            }

            return new TuitionAgencyBranchQueryDto(branch.TuitionAgencyBranchId, branch.TuitionAgencyId, branch.BranchName, branch.BranchAddress);
        }

        public static TuitionAgencyQueryDto ToQueryDto(this TuitionAgency agency)
        {
            if (agency is null)
            {
                throw new ArgumentNullException(nameof(agency));
            }

            return new TuitionAgencyQueryDto(agency.TuitionAgencyId, agency.Name, agency.Branches, agency.Courses);
        }
    }
}
