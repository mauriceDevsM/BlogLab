using BlogLab.Models.Photo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogLab.Repository
{
    public interface IPhotoRepository
    {
        public Task<Photo> InsertAsync(PhotoCreate photocreate, int applicationUserId);

        public Task<Photo> GetAsync(int photoid);
        public Task<List<Photo>> GetAllByUserIdAsync(int applicationUserId);
        public Task<int> DeleteAsync(int photoid);

    }
}
