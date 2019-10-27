namespace Extensions
{
	public static class PathExtension
	{
		public static string Up(this string s)
		{
			if (s.Contains("//") || s.Contains("\\") || s.Contains("\'"))
			{
				UnityEngine.Debug.LogError("wrong path: " + s);
				return s;
			}

			var lastUp = s.LastIndexOf("/");
			var uppedDirectory = s.Substring(0, lastUp);
			return uppedDirectory;
		}
	}
}
