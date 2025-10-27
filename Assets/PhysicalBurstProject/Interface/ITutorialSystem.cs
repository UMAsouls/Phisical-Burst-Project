using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public interface ITutorialSystem
{
    public UniTask Tutorial(TutorialTimingMessage message, CancellationToken token);
    public void Init();
}
