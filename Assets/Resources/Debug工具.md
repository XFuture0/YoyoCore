<b><size=20>Debug 工具</size></b>

<i>Debug.Log 写多了，总会想有个开关能一键关掉它们。DebugTool 就是那个开关。</i>

DebugTool 提供了一个简单的封装，可以通过配置统一控制日志的输出。

<b><size=16>诞生背景</size></b>

在 Unity 开发中，我们经常会在代码中插入各种 Debug 日志来排查问题：

void Start()
{
    Debug.Log("Start 被调用了");
}

void Update()
{
    Debug.Log($"当前位置: {transform.position}");
}

但发布时这些日志会影响性能，需要手动删除或注释掉。这不仅麻烦，而且容易遗漏。

DebugTool 通过封装和配置，让日志的控制变得简单。

<b><size=16>核心功能</size></b>

DebugTool 提供了三个静态方法，分别对应 Unity 的三种日志类型：

<size=14><b>方法列表</b></size>

<b>Log</b> - 对应 Debug.Log，用于普通信息
<b>LogWarning</b> - 对应 Debug.LogWarning，用于警告信息
<b>LogError</b> - 对应 Debug.LogError，用于错误信息

<b><size=16>使用方法</size></b>

<b>基本使用</b>

// 普通日志
DebugTool.Log("游戏开始！");

// 警告日志
DebugTool.LogWarning("血量低于阈值！");

// 错误日志
DebugTool.LogError("网络连接失败！");

<b>条件日志</b>

// 只在条件满足时输出
DebugTool.Log(isDebug, "调试信息");
DebugTool.LogWarning(showWarning, "警告信息");
DebugTool.LogError(hasError, "错误信息");

<b><size=16>配置说明</size></b>

在 Setting 类中配置日志开关：

public static class Setting
{
    // 是否启用日志输出
    public static bool EnableLog = true;
    
    // 是否启用警告输出
    public static bool EnableWarning = true;
    
    // 是否启用错误输出
    public static bool EnableError = true;
}

<b><size=16>完整示例</size></b>

public class Player : MonoBehaviour
{
    private int health = 100;
    
    void Start()
    {
        DebugTool.Log("玩家初始化完成");
    }
    
    void TakeDamage(int damage)
    {
        health -= damage;
        DebugTool.Log($"受到 {damage} 点伤害，当前血量: {health}");
        
        if (health < 30)
        {
            DebugTool.LogWarning("血量过低！");
        }
        
        if (health <= 0)
        {
            DebugTool.LogError("玩家死亡！");
        }
    }
}

<b><size=16>实现原理</size></b>

DebugTool 的核心实现非常简单：

public static class DebugTool
{
    public static void Log(object message)
    {
        if (Setting.EnableLog)
            Debug.Log(message);
    }
    
    public static void LogWarning(object message)
    {
        if (Setting.EnableWarning)
            Debug.LogWarning(message);
    }
    
    public static void LogError(object message)
    {
        if (Setting.EnableError)
            Debug.LogError(message);
    }
}

<b><size=16>注意事项</size></b>

<size=13>1. <b>发布前关闭日志</b>：正式发布前记得在 Setting 中关闭日志开关，避免影响性能

2. <b>错误日志建议保留</b>：即使发布后也建议保留错误日志，便于排查线上问题

3. <b>不要过度使用</b>：日志过多会影响性能，关键位置记录即可</size>
