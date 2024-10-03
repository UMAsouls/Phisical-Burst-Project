using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface MovePosSelectable
{
    public UniTask<bool> MovePosSelect(int id);
}
