using Cysharp.Threading.Tasks;
using System.Collections.Generic; 

public interface IAddressableLoaderService
{ 
    HashSet<string> LoadedLabels { get; }
    UniTask LoadByLabel(string label);
    UniTask LoadByLabels(IEnumerable<string> labels);  
}
