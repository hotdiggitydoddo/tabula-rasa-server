using MoonSharp.Interpreter;

namespace TabulaRasa.Server.Services
{
    public enum ScriptType
    {
        Game,
        Component,
        Command,
        ActionRunner
    }
    public interface IScriptService
    {
        bool Init();
        Script GetScript(ScriptType type, string name);
    }
}