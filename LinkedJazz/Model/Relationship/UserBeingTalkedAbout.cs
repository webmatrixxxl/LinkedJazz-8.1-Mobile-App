using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedJazz.Model.Relationship
{
    public class UserBeingTalkedAbout
    {
        public string Id { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public string Transcript { get; set; }
        public string Value { get; set; }
        public string IdLocals { get; set; }
        public string SourceEncoded { get; set; }
        public string TargetEncoded { get; set; }
    }
}
