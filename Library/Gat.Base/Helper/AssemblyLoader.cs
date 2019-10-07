using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Base
{
    /// <summary>
    /// 組件動態載入器。
    /// </summary>
    public class AssemblyLoader
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="assemblyFile">組件檔案名稱。</param>
        public AssemblyLoader(string assemblyFile)
        {
            this.AssemblyFile = assemblyFile;
        }

        /// <summary>
        /// 組件檔案名稱。
        /// </summary>
        public string AssemblyFile { get; } = string.Empty;

        /// <summary>
        /// 建立指定型別的執行個體。
        /// </summary>
        /// <param name="typeName">物件型別。</param>
        /// <param name="args">建構函式引數。</param>
        public object CreateInstance(string typeName, params object[] args)
        {
            var type = Type.GetType($"{typeName}, {AssemblyFile}");
            return Activator.CreateInstance(type, args);
        }

        /// <summary>
        /// 建立指定型別的執行個體。
        /// </summary>
        /// <param name="typeName">物件型別。</param>
        /// <param name="args">建構函式引數。</param>
        public T CreateInstance<T>(string typeName, params object[] args)
        {
            return (T)this.CreateInstance(typeName, args);
        }

        /// <summary>
        /// 建立指定型別的執行個體。
        /// </summary>
        /// <param name="typeName">物件型別。</param>
        public object CreateInstance(string typeName)
        {
            return this.CreateInstance(typeName, null);
        }

        /// <summary>
        /// 建立指定型別的執行個體。
        /// </summary>
        /// <param name="typeName">物件型別。</param>
        public T CreateInstance<T>(string typeName)
        {
            return (T)this.CreateInstance(typeName);
        }
    }
}
