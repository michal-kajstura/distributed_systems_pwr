package rsi;

import java.util.ArrayList;
import java.util.List;

class Sieve {

    public static List<Integer> primesInRange(int from, int to) {
        var primes = Sieve.sieveOfEratosthenes(to);
        int i;
        for (i = 0; i < primes.size(); i++) {
            if (primes.get(i) > from) break;
        }
        primes = primes.subList(i, primes.size() - 1);
        return primes;
    }

    private static List<Integer> sieveOfEratosthenes(int n) {
        boolean prime[] = new boolean[n+1];
        for(int i=0;i<n;i++)
            prime[i] = true;

        for(int p = 2; p*p <=n; p++) {
            if(prime[p]) {
                for(int i = p*2; i <= n; i += p)
                    prime[i] = false;
            }
        }
        var primes = new ArrayList<Integer>(n / 2);
        for(int i = 2; i <= n; i++) {
            if(prime[i])
                primes.add(i);
        }
        return primes;
    }
}