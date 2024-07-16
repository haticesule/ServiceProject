using Entitiess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IResultRepository
    {
        IQueryable<Result> GetResult();
        Result Result(int id, Result _resultData);
        void positive(int id);
        void negative(int id);
        void CreateOneResult(Result result);
        void UpdateOneResult(Result result);
        void DeleteOnevResult(Result result);

    }
}
