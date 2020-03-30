package rsi;

import java.net.MalformedURLException;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.rmi.Naming;
import java.util.ArrayList;
import java.util.List;

public class Server {

    public static void main(String[] args) {
        var workerAdresses = getWorkerAdresses(2);
        createRegistry();
        createSecurityManager();
        bindWorkers(workerAdresses);
        System.out.println("Press Ctrl+C to stop");
    }

    private static List<String> getWorkerAdresses(int nWorkers) {
        var addresses = new ArrayList<String>(nWorkers);
        for (int i = 0; i < nWorkers; i++) {
            addresses.add("//localhost/" + String.valueOf(i));
        }
        return addresses;
    }

    private static void createRegistry() {
        try {
            LocateRegistry.createRegistry(1099);
        } catch (RemoteException e) {
            System.err.println("Failed to create registry");
            e.printStackTrace();
        }
    }

    private static void createSecurityManager() {
        System.setSecurityManager(new SecurityManager());
    }

    private static void bindWorkers(List<String> addresses) {
        try {
            for (var address: addresses) {
                Naming.rebind(address, new TaskImp());
            }
        } catch (RemoteException ex) {
            System.err.println("Workers cannot be registered");
        } catch (MalformedURLException ex) {
            System.err.println("Wrong url");
        }
    }
}
