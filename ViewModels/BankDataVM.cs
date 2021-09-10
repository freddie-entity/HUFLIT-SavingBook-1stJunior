using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Models;

namespace test.ViewModels
{
    public class BankDataVM
    {
        public IEnumerable<BookType> BookTypes { get; set; }
        public IEnumerable<Interest> Interests { get; set; }
        public IEnumerable<DetailInterest> DetailInterests { get; set; }
        public IEnumerable<Term> Terms { get; set; }
    }
}
