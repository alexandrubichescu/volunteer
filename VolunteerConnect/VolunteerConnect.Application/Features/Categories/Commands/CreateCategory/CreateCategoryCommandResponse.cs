﻿
using VolunteerConnect.Application.Responses;

namespace VolunteerConnect.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandResponse: BaseResponse
    {
        public CreateCategoryCommandResponse(): base()
        {

        }
         
        public CreateCategoryDto Category { get; set; } = default!;
    }
}