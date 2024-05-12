# Technical-Assessment-Task

## Linux OS Knowledge #1 – Networking

### Issue Description

A service is failing to start up correctly. It is usually started as a systemd service called `some_service.service`, but appears to be down. The service is deployed to `/mnt/c3platpersistent/ss/some_service`. The service uses port 61012.

### Task:  How you can figure out that the service is failing to start because a port is blocked?

1. **Check Service Status:**
    ```
    systemctl status some_service.service
    ```
   This command will display the current status of the service. If the service is failing to start, it will be indicated in the output.

2. **Examine Service Logs:**
    - Check logs located in `/var/log/` or as specified in the service configuration. For example:
        ```
        cat /var/log/syslog
        ```
   Look for any error messages or clues as to why the service failed to start.

3. **Verify Port Availability:**
    ```
    netstat -tuln | grep 61012
    or
    ss -tuln | grep 61012
    ```
   This command checks if the port the service is supposed to use (port 61012 in this case) is currently being used by another process. If the port is not in use, it might be blocked.

4. **Check Firewall Rules:**
    - If using iptables:
        ```
        iptables -L
        ```
    - If using firewalld:
        ```
        firewall-cmd --list-all
        ```
   Ensure there are no rules blocking traffic on port 61012. If there are, adjust the firewall settings accordingly to allow traffic on the required port.

5. **SELinux Considerations:**
    - If SELinux is enforcing, it may block service access to the port. Check SELinux policies for denials:
        - Audit log: `/var/log/audit/audit.log`
        - Use `sealert` command.
   Look for any SELinux denials related to the service attempting to access the port. Adjust SELinux policies if necessary to allow the service access to the port.

By following these steps, you can identify if the service failure is due to a blocked port and take appropriate actions to resolve the issue.

-------------------------------------------------------------------------------------

## Linux OS Knowledge #2 – Systemd

### Issue Description

A service is failing to start up correctly. It is usually started as a systemd service called 
some_service.service, but looks to be down. It seems the unitfile has some typo. The service is deployed 
to /mnt/c3platpersistent/ss/some_service.

### Task: Try to find out the unitfile and correct it. Write the corresponding linux commands. Then start the service  and make sure that it’s up and running.


1. **Locate the Unit File:**
   - Use the `find` command to search for the unit file:
     ```bash
     find /etc/systemd /lib/systemd /run/systemd /usr/lib/systemd -name some_service.service
     ```

2. **Edit the Unit File:**
   - Once located, edit the unit file to correct any typos:
     ```bash
     sudo nano /path/to/some_service.service
     ```

3. **Reload systemd:**
   - After editing the unit file, reload systemd to apply changes:
     ```bash
     sudo systemctl daemon-reload
     ```

4. **Start the Service:**
   - Start the corrected service:
     ```bash
     sudo systemctl start some_service.service
     ```

5. **Verify Service Status:**
   - Check the status of the service to ensure it's running:
     ```bash
     sudo systemctl status some_service.service
     ```

6. **Enable the Service (Optional):**
   - If desired, enable the service to start at boot:
     ```bash
     sudo systemctl enable some_service.service
     ```

-----------------------------------------------------------------------------

# Linux OS Configuration Tasks

## Task 1: Configure a Static IP Address

To configure a static IP address (e.g., 192.168.1.100) on a Linux machine using the command line:

1. Open the network configuration file using a text editor:
   ```bash
   sudo nano /etc/network/interfaces

2. Add the following lines to the file, replacing eth0 with your network interface name:
   auto eth0
iface eth0 inet static
    address 192.168.1.100
    netmask 255.255.255.0
    gateway 192.168.1.1
   
3. Save the file and exit the text editor.
4. Restart the networking service:
sudo systemctl restart networking


## Task 2: Create a systemd Service
To create a systemd service called "test-service" that executes a shell script located at /opt/test-script.sh on boot:

1. Create the shell script /opt/test-script.sh with your desired commands.
2. Create a systemd service unit file:
sudo nano /etc/systemd/system/test-service.service


Add the following lines to the file:

[Unit]
Description=Test Service

After=network.target


[Service]
Type=simple

ExecStart=/bin/bash /opt/test-script.sh


[Install]
WantedBy=multi-user.target

4. Save the file and exit the text editor.
5. Reload systemd to apply changes:
sudo systemctl daemon-reload
6. Enable the service to start on boot:
sudo systemctl enable test-service.service


## Task 3: Shell Script to Restart a Process

1. Create a shell script (e.g., restart_process.sh) with the following content:

#!/bin/bash

if pgrep -x "process_name" >/dev/null; then

    echo "Process is running."
    
else

    echo "Process is not running. Restarting..."
    systemctl restart process_name.service
    
fi


2. Replace "process_name" with the name of the process you want to monitor and process_name.service with the corresponding systemd service name.


3. Save the script and make it executable:
chmod +x restart_process.sh

4. You can now execute the script whenever needed to check and restart the specified process.


---------------------------------------------------------------------------------------

# C#/SpecFlow Knowledge – Keywords Specification & Implementation

Check my code please: 

















   








