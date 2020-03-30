package rsi;

import java.io.Serializable;

public class ReturnType implements Serializable {
    public int count;
    public int max;
    public double med;

    public ReturnType(int count, int max, double med) {
        this.count = count;
        this.max = max;
        this.med = med;
    }
}
