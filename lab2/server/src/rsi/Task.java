package rsi;

import java.rmi.Remote;
import java.rmi.RemoteException;

public interface Task extends Remote {
    ReturnType compute(int from, int to) throws RemoteException;
}
