package rsi;

import java.rmi.Remote;
import java.rmi.RemoteException;

public interface Task extends Remote {
    ReturnType compute(long from, long to) throws RemoteException;
}
