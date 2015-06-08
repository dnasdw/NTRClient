using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ntrclient {
	public class ScriptHelper {
        public void bpadd(uint addr, string type = "code.once") {
            if (type == "arm") {
                Program.ntrClient.sendEmptyPacket(11, addr, 0, 0x101);
            }
            else if (type == "thumb") {
                Program.ntrClient.sendEmptyPacket(11, addr, 1, 0x101);
            }
        }

        public void bpdis(uint id) {
            Program.ntrClient.sendEmptyPacket(11, id, 0, 0x103);
        }

        public void bpena(uint id) {
            Program.ntrClient.sendEmptyPacket(11, id, 0, 0x102);
        }

        public void resume() {
            Program.ntrClient.sendEmptyPacket(11, 0x100, 0, 0x141);
        }

        public void continueprocess() {
            Program.ntrClient.sendEmptyPacket(11, 0, 0, 4);
        }

        public void step() {
            Program.ntrClient.sendEmptyPacket(11, 0x101, 0, 0x141);
        }

		public void connect(string host, int port) {
			Program.ntrClient.setServer(host, port);
			Program.ntrClient.connectToServer();
		}

		public void reload() {
			Program.ntrClient.sendReloadPacket();
		}
		
		public void listprocess() {
			Program.ntrClient.sendEmptyPacket(5);
		}

		public void listthread(int pid) {
			Program.ntrClient.sendEmptyPacket(7, (uint) pid);
		}

		public void attachprocess(int pid, uint patchAddr = 0) {
			Program.ntrClient.sendEmptyPacket(6, (uint) pid, patchAddr);
		}

        public void queryhandle(int pid) {
            Program.ntrClient.sendEmptyPacket(12, (uint)pid );
        }

		public void memlayout(int pid) {
			Program.ntrClient.sendEmptyPacket(8, (uint)pid);
		}

		public void disconnect() {
			Program.ntrClient.disconnect();
		}

		public void sayhello() {
			Program.ntrClient.sendHelloPacket();
		}

		public void data(uint addr, uint size = 0x100, int pid = -1, string filename = null) {
			if (filename == null && size > 1024) {
				size = 1024;
			}
			Program.ntrClient.sendReadMemPacket(addr, size, (uint) pid, filename);
		}

		public void write(uint addr, byte[] buf, int pid=-1) {
			Program.ntrClient.sendWriteMemPacket(addr, (uint)pid, buf);
		}

		public void sendfile(String localPath, String remotePath) {
			FileStream fs = new FileStream(localPath, FileMode.Open);
			byte[] buf = new byte[fs.Length];
			fs.Read(buf, 0, buf.Length);
			fs.Close();
			Program.ntrClient.sendSaveFilePacket(remotePath, buf);
		}

        public void wpadd(uint addr, string wide = "dw", string type = "ls") {
            uint byteAddressSelectFlag, loadStoreFlag;
            if (wide == "dw") {
                addr = (uint)(addr & ~3);
                byteAddressSelectFlag = 0xF;
            }
            else if (wide == "w") {
                if ((addr & 0x3) == 3)
                    addr = (uint)(addr & ~1);
                byteAddressSelectFlag = (uint)(0x3 << ((int)addr & 0x3));
            }
            else if (wide == "b")
                byteAddressSelectFlag = (uint)(0x1 << ((int)addr & 0x3));
            else
                return;
            if (type == "l")
                loadStoreFlag = 0x1;
            else if (type == "s")
                loadStoreFlag = 0x2;
            else if (type == "ls")
                loadStoreFlag = 0x3;
            else
                return;
            UInt32[] args = new UInt32[16];
            args[0] = addr;
            args[1] = loadStoreFlag;
            args[2] = 0x121;
            args[3] = byteAddressSelectFlag;
            Program.ntrClient.sendPacket(0, 11, args, 0);
        }

        public void wpdis(uint id) {
            Program.ntrClient.sendEmptyPacket(11, id, 0, 0x123);
        }

        public void wpena(uint id) {
            Program.ntrClient.sendEmptyPacket(11, id, 0, 0x122);
        }

        public void wpdel(uint id) {
            Program.ntrClient.sendEmptyPacket(11, id, 0, 0x124);
        }

        public void display(string type)
        {
            if (type == "double") {
                Program.ntrClient.sendEmptyPacket(11, 0, 0, 0x162);
            }
            else if (type == "single") {
                Program.ntrClient.sendEmptyPacket(11, 0, 0, 0x161);
            }
            else if (type == "breakpoint") {
                Program.ntrClient.sendEmptyPacket(11, 0, 0, 0x163);
            }
            else if (type == "watchpoint") {
                Program.ntrClient.sendEmptyPacket(11, 0, 0, 0x164);
            }
        }
	}
}
