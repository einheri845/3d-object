using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace _3D_letter
{
    class figure
    {
        public struct Point3D
        {
            public float x;
            public float y;
            public float z;
        }

        public Point3D[] frontSide3D;
        public Point3D[] backSide3D;

        public PointF[] frontSide2D;
        public PointF[] backSide2D;

        private Point3D[] backupFront3D;
        private Point3D[] backupBack3D;

        public float scaleX = 1;
        public float scaleY = 1;
        public float scaleZ = 1;

        public float angleX = 0;
        public float angleY = 0;
        public float angleZ = 0;

        public float translateX = 0;
        public float translateY = 0;
        public float translateZ = 0;

        private float oldSX = 1;
        private float oldSY = 1;
        private float oldSZ = 1;

        private float oldAX = 0;
        private float oldAY = 0;
        private float oldAZ = 0;

        private float oldTX = 0;
        private float oldTY = 0;
        private float oldTZ = 0;

        private float depthOfPers = 2000;

        public int points = 0;
        private string path = "figure.txt";

        public figure()
        {
            points = File.ReadAllLines(path).Length;
            frontSide2D = new PointF[points];
            backSide2D = new PointF[points];
            frontSide3D = new Point3D[points];
            backSide3D = new Point3D[points];
            backupFront3D = new Point3D[points];
            backupBack3D = new Point3D[points];

            using (StreamReader file = new StreamReader(path, System.Text.Encoding.Default))
            {
                int i = 0;
                string coord;
                while ((coord = file.ReadLine()) != null)
                {
                    string[] xy = coord.Split(new Char[] { ' ' });
                    frontSide3D[i].x = Convert.ToSingle(xy[0]) * 20;
                    frontSide3D[i].y = Convert.ToSingle(xy[1]) * -20;
                    frontSide3D[i].z = 0;
                    i++;
                }
                file.Close();
            }
           

            backSide3D = (Point3D[])frontSide3D.Clone();
            for (int i = 0; i < points; i++)
            {
                backSide3D[i].z = 30;
            }

            backupFront3D = (Point3D[])frontSide3D.Clone();
            backupBack3D = (Point3D[])backSide3D.Clone();
            To2D();
        }

        public void To2D()
        {
            for (int i = 0; i < points; i++)
            {
                frontSide2D[i].X = frontSide3D[i].x / (1 - (frontSide3D[i].z / depthOfPers));
                frontSide2D[i].Y = frontSide3D[i].y / (1 - (frontSide3D[i].z / depthOfPers));

                backSide2D[i].X = backSide3D[i].x / (1 - (backSide3D[i].z / depthOfPers));
                backSide2D[i].Y = backSide3D[i].y / (1 - (backSide3D[i].z / depthOfPers));
            }
        }

        private float Rad(float angle, float newAngle)
        {
            return (float)((newAngle - angle) * Math.PI) / 180;
        }

        public void Transfomation()
        {
            float cosAX = (float)Math.Cos(Rad(angleX, oldAX));
            float cosAY = (float)Math.Cos(Rad(angleY, oldAY));
            float cosAZ = (float)Math.Cos(Rad(angleZ, oldAZ));

            float sinAX = (float)Math.Sin(Rad(angleX, oldAX));
            float sinAY = (float)Math.Sin(Rad(angleY, oldAY));
            float sinAZ = (float)Math.Sin(Rad(angleZ, oldAZ));

            oldAX = angleX;
            oldAY = angleY;
            oldAZ = angleZ;

            for (int i = 0; i < points; i++)
            {
                float x = (scaleX / oldSX) * (frontSide3D[i].x * cosAX * cosAZ + frontSide3D[i].y * (-sinAX * sinAY * cosAZ - sinAZ * cosAX) + frontSide3D[i].z * (sinAX * sinAZ - sinAY * cosAX * cosAZ)) + (translateX - oldTX);
                float y = (scaleY / oldSY) * (frontSide3D[i].x * sinAZ * cosAY + frontSide3D[i].y * (-sinAX * sinAY * sinAZ + cosAX * cosAZ) + frontSide3D[i].z * (-sinAX * cosAZ - sinAY * sinAZ * cosAX)) + (translateY - oldTY);
                float z = (scaleZ / oldSZ) * (frontSide3D[i].x * sinAY + frontSide3D[i].y * sinAX * cosAY + frontSide3D[i].z * cosAX * cosAY) + (translateZ - oldTZ);

                frontSide3D[i].x = x;
                frontSide3D[i].y = y;
                frontSide3D[i].z = z;
                
                x = (scaleX / oldSX) * (backSide3D[i].x * cosAX * cosAZ + backSide3D[i].y * (-sinAX * sinAY * cosAZ - sinAZ * cosAX) + backSide3D[i].z * (sinAX * sinAZ - sinAY * cosAX * cosAZ)) + (translateX - oldTX);
                y = (scaleY / oldSY) * (backSide3D[i].x * sinAZ * cosAY + backSide3D[i].y * (-sinAX * sinAY * sinAZ + cosAX * cosAZ) + backSide3D[i].z * (-sinAX * cosAZ - sinAY * sinAZ * cosAX)) + (translateY - oldTY);
                z = (scaleZ / oldSZ) * (backSide3D[i].x * sinAY + backSide3D[i].y * sinAX * cosAY + backSide3D[i].z * cosAX * cosAY) + (translateZ - oldTZ);

                backSide3D[i].x = x;
                backSide3D[i].y = y;
                backSide3D[i].z = z;

            }
            To2D();

            oldSX = scaleX;
            oldSY = scaleY;
            oldSZ = scaleZ;

            oldTX = translateX;
            oldTY = translateY;
            oldTZ = translateZ;
        }

        public void BackUp()
        {
            scaleX = 1;
            scaleY = 1;
            scaleZ = 1;

            angleX = 0;
            angleY = 0;
            angleZ = 0;

            translateX = 0;
            translateY = 0;
            translateZ = 0;

            oldSX = 1;
            oldSY = 1;
            oldSZ = 1;

            oldAX = 0;
            oldAY = 0;
            oldAZ = 0;

            oldTX = 0;
            oldTY = 0;
            oldTZ = 0;

            frontSide3D = (Point3D[])backupFront3D.Clone();
            backSide3D = (Point3D[])backupBack3D.Clone();
            To2D();
        }
    }
}
