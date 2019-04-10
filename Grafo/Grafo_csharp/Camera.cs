
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grafo_csharp
{
    public class Camera
    {
		public float fov;
        public double dir_lat;
        public double dir_long;
        public float dist;
        public double[] center;//vertice com x,y,z
        
        public Camera() {
	        dir_lat=Math.PI/4;
	        dir_long=-Math.PI/4;
	        fov=60;
	        dist=200;

	        center[0]=0;
	        center[1]=0;
	        center[2]=0;
        }

        public float getFOV() {
	        return this.fov;
        }

        public double getDirLat()
        {
	        return this.dir_lat;
        }

        public double getDirLong()
        {
	        return this.dir_long;
        }

        public float getDist()
        {
	        return this.dist;
        }
    }
}
