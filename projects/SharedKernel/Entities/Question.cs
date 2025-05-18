using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Entities;

public class Question
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;

    public Guid PollId { get; set; }
    public Poll Poll { get; set; }

    public ICollection<Option> Options { get; set; } = new List<Option>();
}
