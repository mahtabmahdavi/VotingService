using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Entities;

public class Vote
{
    public Guid Id { get; set; }

    public Guid OptionId { get; set; }
    public Option Option { get; set; }

    public string VoterId { get; set; } = string.Empty;
    public DateTime VotedAt { get; set; } = DateTime.UtcNow;
}
