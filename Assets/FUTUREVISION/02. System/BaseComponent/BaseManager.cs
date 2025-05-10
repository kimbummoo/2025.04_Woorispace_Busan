/*
 * DATE     : 2024.11.27
 * AUTHOR   : Kim Bum Moo
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FUTUREVISION
{
    public class BaseManager<FinalManagerClass> : BaseSingleton<FinalManagerClass>
        where FinalManagerClass : BaseManager<FinalManagerClass>
    {
        public virtual void Initialize()
        {
        }
    }
}