package rsi;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.ArrayList;
import java.util.List;

public class TaskImp extends UnicastRemoteObject
    implements Task {

    public TaskImp() throws RemoteException {
        super();
    }

    @Override
    public ReturnType compute(int from, int to) throws RemoteException {
        var primes = Sieve.primesInRange(from, to);
        var median = computeMedian(primes);
        return new ReturnType(
                primes.size(),
                primes.get(primes.size() - 1),
                median
        );
    }

    private double computeMedian(List<Integer> primes) {
        int s = primes.size();
        return (s % 2 == 0) ? ((double) primes.get(s / 2) + primes.get(s / 2 - 1)) / 2
                            : primes.get(s / 2);
    }
}
