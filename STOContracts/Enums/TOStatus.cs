using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STOContracts.Enums
{
    /// <summary>
    /// Статус ТО
    /// </summary>
    public enum TOStatus
    {
        Принят = 0,

        Выполняется = 1,

        Готов = 2,

        Выдан_клиенту = 3
    }
}
