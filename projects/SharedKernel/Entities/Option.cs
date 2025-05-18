using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Entities;

public class Option
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;

    public Guid QuestuionId { get; set; }
    public Question Question { get; set; }

    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
