using UnityEngine;
using NoName.Utilities;
using PathCreation;
using PathCreation.Examples;

namespace NoName
{
	public class PathManager : Singleton<PathManager>
	{
		#region SerializedFields

		[SerializeField]
		private PathCreator path;

		[SerializeField]
		private RoadMeshCreator pathMesh;

		#endregion

		#region Variables

		#endregion

		#region Props

		public PathCreator Path => path;
		public RoadMeshCreator PathMesh => pathMesh;


		#endregion

		#region Unity Methods

		#endregion

		#region Methods

		#endregion

		#region Callbacks

		#endregion
	}
}