using Application.Dtos.Person;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Person
{
    public interface IPersonValidatorService
    {
        void ValidateForSave(PersonForSaveDto personForCreation);
        Task ValidateForSave(int id, PersonForSaveDto personForCreation);
    }    
}
