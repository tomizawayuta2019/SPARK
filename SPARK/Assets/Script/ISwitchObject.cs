using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwitchObject {
    /// <summary>
    /// on off 切り替え
    /// </summary>
    /// <param name="value"></param>
    void SetValue(bool value);
}
