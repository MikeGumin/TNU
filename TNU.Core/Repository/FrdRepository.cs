using System.Collections.ObjectModel;
using TNU.Core.Models;

namespace TNU.Core.Repository;

public static class FrdRepository
{
    public static ObservableCollection<FrdModel> FinishedFrd { get; set; } = new();
}