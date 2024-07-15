import pandas as pd
import numpy as np
import matplotlib.pyplot as plt

# Parameters
total_days = 80
initial_days= 20
crash_day = 72

# Generate stock data
np.random.seed(6)
stock_prices = np.cumprod(1 + (np.random.normal(0.05, 0.1, initial_days)))
stock_prices2 = np.cumprod(1 + (np.random.normal(0.05, 0.25, crash_day - initial_days)))
crash = np.cumprod(1 - np.random.normal(0.5, 0.1, total_days - crash_day))
stock_prices = np.concatenate((stock_prices, stock_prices2*stock_prices[-1], crash * stock_prices[-1]))

print(stock_prices)
# Create a DataFrame
dates = pd.date_range(start='2024-01-01', periods=total_days, freq='D')
stock_data = pd.DataFrame({'Date': dates, 'Stock Price': stock_prices})

# Plot the stock data
plt.figure(figsize=(10, 6))
plt.plot(stock_data['Date'], stock_data['Stock Price'], label='Stock Price')
plt.axvline(stock_data['Date'][crash_day], color='r', linestyle='--', label='Crash Day')
plt.title('Stock Price Over Time')
plt.xlabel('Date')
plt.ylabel('Stock Price')
plt.legend()
plt.grid(True)
plt.show()

# Display the DataFrame
import ace_tools as tools; tools.display_dataframe_to_user(name="Stock Data", dataframe=stock_data)
