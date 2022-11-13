using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IFacultyDal
    {
        FacultyDTO AddFaculty(FacultyDTO faculty);

        FacultyDTO GetFacultyById(string id);
        FacultyDTO GetFacultyByName(string name);
        List<FacultyDTO> GetAllFaculties();
        bool RemoveFacultyById(string id);
        FacultyDTO UpdateFaculty(FacultyDTO faculty);
    }
}
