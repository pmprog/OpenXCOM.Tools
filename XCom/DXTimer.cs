/*using System;
using System.Runtime.InteropServices;

namespace XCom
{
	public class DXTimer
    {
		private static Random gen = new Random();
	
		#region imported functions

		/// <summary>
		/// This function retrieves the system's performance 
		/// counter frequency.
		/// </summary>
		[DllImport("kernel32")]
		private static extern bool QueryPerformanceFrequency(ref long Frequency);

		/// <summary>
		/// This function retrieves the current number of
		/// ticks for this system.
		/// </summary>
		[DllImport("kernel32")]
		private static extern bool QueryPerformanceCounter(ref long Count);
		
		#endregion

		/// <summary>
		/// The last number of ticks.
		/// </summary>
		private static long   lLastTime            = 0;		

		/// <summary>
		/// The current number of ticks.
		/// </summary>
		private static long   lCurrentTime         = 0;

		/// <summary>
		/// The number of ticks per second for this system.
		/// This will be a constant value.
		/// </summary>
		private static long   lTicksPerSecond      = 0;

		/// <summary>
		/// Indicates if the Timer is initialized
		/// </summary>
		private static bool   bInitialized         = false;

		/// <summary>
		/// The elapsed seconds since the last 
		/// GetElapsedSeconds() call.
		/// </summary>
		private static double dElapsedSeconds      = 0.0;

		/// <summary>
		/// The elapsed milliseconds since the last
		/// GetElapsedMilliseconds() call.
		/// </summary>
		private static double dElapsedMilliseconds = 0.0;

		/// <summary>
		/// Property to query the ticks per second
		/// for this system (Timer has to be initialized).
		/// </summary>
		public static long TicksPerSecond
		{
			get
			{	
				return lTicksPerSecond;
			}
		}

		public static int RandInt()
		{
			return gen.Next();
		}

		public static double GetCurrMillis()
		{
			// Check if initialized
			if (!bInitialized)
			{
				throw new Exception("Timer not initialized!");
			}
			
			// Get current number of ticks
			QueryPerformanceCounter(ref lCurrentTime);

			// Return milliseconds
			return (double)lCurrentTime/(double)lTicksPerSecond*1000.0;
		}

		/// <summary>
		/// The initialization of the timer. Tries to query
		/// performance frequency and determines if performance
		/// counters are supported by this system.
		/// </summary>
		public static void Init()
		{
			// Try to read frequency. If this fails, performance
			// counters are not supported.
			if (!QueryPerformanceFrequency(ref lTicksPerSecond))				
			{
				throw new Exception("Performance Counter not supported on this system!");
			}

			// Initialization successful
			bInitialized = true;
		}

		/// <summary>
		/// Starts the Timer. This set the initial time value.
		/// Timer has to be initialized for this.
		/// </summary>
		public static void Start()
		{
			// Check if initialized
			if (!bInitialized)
			{
				throw new Exception("Timer not initialized!");
			}

			// Initialize time value
			QueryPerformanceCounter(ref lLastTime); 
		}

		/// <summary>
		/// Gets the elapsed milliseconds since the last
		/// call to this function. Timer has to be initialized
		/// for this.
		/// </summary>
		/// <returns>The number of milliseconds.</returns>
		public static double GetElapsedMilliseconds()
		{
			// Check if initialized
			if (!bInitialized)
			{
				throw new Exception("Timer not initialized!");
			}
			
			// Get current number of ticks
			QueryPerformanceCounter(ref lCurrentTime);

			// Calculate number of milliseconds since last call
			dElapsedMilliseconds = ((double)(lCurrentTime - lLastTime) / (double)lTicksPerSecond) * 1000.0;

			// Store current number of ticks for next call
			lLastTime = lCurrentTime;

			// Return milliseconds
			return dElapsedMilliseconds;
		}

		/// <summary>
		/// Gets the elapsed seconds since the last call
		/// to this function. Timer has to be initialized for this.
		/// </summary>
		/// <returns>The number of seconds.</returns>
		public static double GetElapsedSeconds()
		{
			// Check if initialized
			if (!bInitialized)
			{	
				throw new Exception("Timer not initialized!");
			}

			// Get current number of ticks
			QueryPerformanceCounter(ref lCurrentTime);

			// Calculate elapsed seconds
			dElapsedSeconds = (double)(lCurrentTime - lLastTime) / (double)lTicksPerSecond;

			// Store current number of ticks for next call
			lLastTime = lCurrentTime;

			// Return number of seconds
			return dElapsedSeconds;
		}
  	}
}
*/