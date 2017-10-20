namespace E_Library
{
    class CurrentUser
    {
        public CurrentUser()
        {

        }
        private int ID;
        private string name, password;
        private byte securityLevel;
        public void setID(int ID) { this.ID = ID; }
        public void setName(string name) { this.name = name; }
        public void setPassword(string password) { this.password = password; }
        public void setSecurityLevel(byte securityLevel) { this.securityLevel = securityLevel; }

        public int getID() { return ID; }
        public string getName() { return name; }
        public string getPassword() { return password; }
        public byte getSecuritytLevel() { return securityLevel; }

    }
}
