
using Android.OS;
using Java.Util.Concurrent.Locks;
using Java.Lang.Ref;
using Java.Lang;

//using VisibleForTesting = Android.Support.Annotation.VisibleForTesting;

namespace Com.Loonytone.Droid.Banners
{



    //JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
    //ORIGINAL LINE: @SuppressWarnings("unused") public class WeakHandler
    public class WeakHandler
	{
		private bool InstanceFieldsInitialized = false;

		private void InitializeInstanceFields()
		{
			mRunnables = new ChainedRef(mLock, null);
		}

		private readonly Handler.ICallback mCallback; // hard reference to ICallback. We need to keep callback in memory
		private readonly ExecHandler mExec;
		private ILock mLock = new ReentrantLock();
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("ConstantConditions") @VisibleForTesting final ChainedRef mRunnables = new ChainedRef(mLock, null);
		internal ChainedRef mRunnables;

		/// <summary>
		/// Default constructor associates this handler with the <seealso cref="Looper"/> for the
		/// current thread.
		/// 
		/// If this thread does not have a looper, this handler won't be able to receive messages
		/// so an exception is thrown.
		/// </summary>
		public WeakHandler()
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			mCallback = null;
			mExec = new ExecHandler();
		}

		/// <summary>
		/// Constructor associates this handler with the <seealso cref="Looper"/> for the
		/// current thread and takes a callback interface in which you can handle
		/// messages.
		/// 
		/// If this thread does not have a looper, this handler won't be able to receive messages
		/// so an exception is thrown.
		/// </summary>
		/// <param name="callback"> The callback interface in which to handle messages, or null. </param>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: public WeakHandler(@Nullable android.os.Handler.ICallback callback)
		public WeakHandler(Handler.ICallback callback)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			mCallback = callback; // Hard referencing body

            mExec = new ExecHandler(new WeakReference((Java.Lang.Object)callback)); // Weak referencing inside ExecHandler
		}

		/// <summary>
		/// Use the provided <seealso cref="Looper"/> instead of the default one.
		/// </summary>
		/// <param name="looper"> The looper, must not be null. </param>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: public WeakHandler(@NonNull android.os.Looper looper)
		public WeakHandler(Looper looper)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			mCallback = null;
			mExec = new ExecHandler(looper);
		}

		/// <summary>
		/// Use the provided <seealso cref="Looper"/> instead of the default one and take a callback
		/// interface in which to handle messages.
		/// </summary>
		/// <param name="looper"> The looper, must not be null. </param>
		/// <param name="callback"> The callback interface in which to handle messages, or null. </param>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: public WeakHandler(@NonNull android.os.Looper looper, @NonNull android.os.Handler.ICallback callback)
		public WeakHandler(Looper looper, Handler.ICallback callback)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			mCallback = callback;
			mExec = new ExecHandler(looper, new WeakReference((Java.Lang.Object)callback));
		}

		/// <summary>
		/// Causes the Runnable r to be added to the message queue.
		/// The runnable will be run on the thread to which this handler is
		/// attached.
		/// </summary>
		/// <param name="r"> The Runnable that will be executed.
		/// </param>
		/// <returns> Returns true if the Runnable was successfully placed in to the
		///         message queue.  Returns false on failure, usually because the
		///         looper processing the message queue is exiting. </returns>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: public final boolean post(@NonNull Runnable r)
		public bool post(Runnable r)
		{
			return mExec.Post(wrapRunnable(r));
		}

		/// <summary>
		/// Causes the Runnable r to be added to the message queue, to be run
		/// at a specific time given by <var>uptimeMillis</var>.
		/// <b>The time-base is <seealso cref="android.os.SystemClock#uptimeMillis"/>.</b>
		/// The runnable will be run on the thread to which this handler is attached.
		/// </summary>
		/// <param name="r"> The Runnable that will be executed. </param>
		/// <param name="uptimeMillis"> The absolute time at which the callback should run,
		///         using the <seealso cref="android.os.SystemClock#uptimeMillis"/> time-base.
		/// </param>
		/// <returns> Returns true if the Runnable was successfully placed in to the
		///         message queue.  Returns false on failure, usually because the
		///         looper processing the message queue is exiting.  Note that a
		///         result of true does not mean the Runnable will be processed -- if
		///         the looper is quit before the delivery time of the message
		///         occurs then the message will be dropped. </returns>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: public final boolean PostAtTime(@NonNull Runnable r, long uptimeMillis)
		public bool PostAtTime(Runnable r, long uptimeMillis)
		{
			return mExec.PostAtTime(wrapRunnable(r), uptimeMillis);
		}

		/// <summary>
		/// Causes the Runnable r to be added to the message queue, to be run
		/// at a specific time given by <var>uptimeMillis</var>.
		/// <b>The time-base is <seealso cref="android.os.SystemClock#uptimeMillis"/>.</b>
		/// The runnable will be run on the thread to which this handler is attached.
		/// </summary>
		/// <param name="r"> The Runnable that will be executed. </param>
		/// <param name="uptimeMillis"> The absolute time at which the callback should run,
		///         using the <seealso cref="android.os.SystemClock#uptimeMillis"/> time-base.
		/// </param>
		/// <returns> Returns true if the Runnable was successfully placed in to the
		///         message queue.  Returns false on failure, usually because the
		///         looper processing the message queue is exiting.  Note that a
		///         result of true does not mean the Runnable will be processed -- if
		///         the looper is quit before the delivery time of the message
		///         occurs then the message will be dropped.
		/// </returns>
		/// <seealso cref= android.os.SystemClock#uptimeMillis </seealso>
		public bool PostAtTime(Runnable r, Object token, long uptimeMillis)
		{
            return mExec.PostAtTime(wrapRunnable(r), token, uptimeMillis);
		}

		/// <summary>
		/// Causes the Runnable r to be added to the message queue, to be run
		/// after the specified amount of time elapses.
		/// The runnable will be run on the thread to which this handler
		/// is attached.
		/// </summary>
		/// <param name="r"> The Runnable that will be executed. </param>
		/// <param name="delayMillis"> The delay (in milliseconds) until the Runnable
		///        will be executed.
		/// </param>
		/// <returns> Returns true if the Runnable was successfully placed in to the
		///         message queue.  Returns false on failure, usually because the
		///         looper processing the message queue is exiting.  Note that a
		///         result of true does not mean the Runnable will be processed --
		///         if the looper is quit before the delivery time of the message
		///         occurs then the message will be dropped. </returns>
		public bool PostDelayed(Runnable r, long delayMillis)
		{
			return mExec.PostDelayed(wrapRunnable(r), delayMillis);
		}

		/// <summary>
		/// Posts a message to an object that implements Runnable.
		/// Causes the Runnable r to executed on the next iteration through the
		/// message queue. The runnable will be run on the thread to which this
		/// handler is attached.
		/// <b>This method is only for use in very special circumstances -- it
		/// can easily starve the message queue, cause ordering problems, or have
		/// other unexpected side-effects.</b>
		/// </summary>
		/// <param name="r"> The Runnable that will be executed.
		/// </param>
		/// <returns> Returns true if the message was successfully placed in to the
		///         message queue.  Returns false on failure, usually because the
		///         looper processing the message queue is exiting. </returns>
		public bool postAtFrontOfQueue(Runnable r)
		{
			return mExec.PostAtFrontOfQueue(wrapRunnable(r));
		}

		/// <summary>
		/// Remove any pending posts of Runnable r that are in the message queue.
		/// </summary>
		public void RemoveCallbacks(Runnable r)
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final WeakRunnable runnable = mRunnables.remove(r);
			WeakRunnable runnable = mRunnables.remove(r);
			if (runnable != null)
			{
				mExec.RemoveCallbacks(runnable);
			}
		}

		/// <summary>
		/// Remove any pending posts of Runnable <var>r</var> with Object
		/// <var>token</var> that are in the message queue.  If <var>token</var> is null,
		/// all callbacks will be removed.
		/// </summary>
		public void RemoveCallbacks(Runnable r, Object token)
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final WeakRunnable runnable = mRunnables.remove(r);
			WeakRunnable runnable = mRunnables.remove(r);
			if (runnable != null)
			{
				mExec.RemoveCallbacks(runnable, token);
			}
		}

		/// <summary>
		/// Pushes a message onto the end of the message queue after all pending messages
		/// before the current time. It will be received in callback,
		/// in the thread attached to this handler.
		/// </summary>
		/// <returns> Returns true if the message was successfully placed in to the
		///         message queue.  Returns false on failure, usually because the
		///         looper processing the message queue is exiting. </returns>
		public bool sendMessage(Message msg)
		{
			return mExec.SendMessage(msg);
		}

		/// <summary>
		/// Sends a Message containing only the what value.
		/// </summary>
		/// <returns> Returns true if the message was successfully placed in to the
		///         message queue.  Returns false on failure, usually because the
		///         looper processing the message queue is exiting. </returns>
		public bool sendEmptyMessage(int what)
		{
			return mExec.SendEmptyMessage(what);
		}

		/// <summary>
		/// Sends a Message containing only the what value, to be delivered
		/// after the specified amount of time elapses. </summary>
		/// <seealso cref= #sendMessageDelayed(android.os.Message, long)
		/// </seealso>
		/// <returns> Returns true if the message was successfully placed in to the
		///         message queue.  Returns false on failure, usually because the
		///         looper processing the message queue is exiting. </returns>
		public bool sendEmptyMessageDelayed(int what, long delayMillis)
		{
			return mExec.SendEmptyMessageDelayed(what, delayMillis);
		}

		/// <summary>
		/// Sends a Message containing only the what value, to be delivered
		/// at a specific time. </summary>
		/// <seealso cref= #sendMessageAtTime(android.os.Message, long)
		/// </seealso>
		/// <returns> Returns true if the message was successfully placed in to the
		///         message queue.  Returns false on failure, usually because the
		///         looper processing the message queue is exiting. </returns>
		public bool sendEmptyMessageAtTime(int what, long uptimeMillis)
		{
			return mExec.SendEmptyMessageAtTime(what, uptimeMillis);
		}

		/// <summary>
		/// Enqueue a message into the message queue after all pending messages
		/// before (current time + delayMillis). You will receive it in
		/// callback, in the thread attached to this handler.
		/// </summary>
		/// <returns> Returns true if the message was successfully placed in to the
		///         message queue.  Returns false on failure, usually because the
		///         looper processing the message queue is exiting.  Note that a
		///         result of true does not mean the message will be processed -- if
		///         the looper is quit before the delivery time of the message
		///         occurs then the message will be dropped. </returns>
		public bool sendMessageDelayed(Message msg, long delayMillis)
		{
			return mExec.SendMessageDelayed(msg, delayMillis);
		}

		/// <summary>
		/// Enqueue a message into the message queue after all pending messages
		/// before the absolute time (in milliseconds) <var>uptimeMillis</var>.
		/// <b>The time-base is <seealso cref="android.os.SystemClock#uptimeMillis"/>.</b>
		/// You will receive it in callback, in the thread attached
		/// to this handler.
		/// </summary>
		/// <param name="uptimeMillis"> The absolute time at which the message should be
		///         delivered, using the
		///         <seealso cref="android.os.SystemClock#uptimeMillis"/> time-base.
		/// </param>
		/// <returns> Returns true if the message was successfully placed in to the
		///         message queue.  Returns false on failure, usually because the
		///         looper processing the message queue is exiting.  Note that a
		///         result of true does not mean the message will be processed -- if
		///         the looper is quit before the delivery time of the message
		///         occurs then the message will be dropped. </returns>
		public virtual bool sendMessageAtTime(Message msg, long uptimeMillis)
		{
			return mExec.SendMessageAtTime(msg, uptimeMillis);
		}

		/// <summary>
		/// Enqueue a message at the front of the message queue, to be processed on
		/// the next iteration of the message loop.  You will receive it in
		/// callback, in the thread attached to this handler.
		/// <b>This method is only for use in very special circumstances -- it
		/// can easily starve the message queue, cause ordering problems, or have
		/// other unexpected side-effects.</b>
		/// </summary>
		/// <returns> Returns true if the message was successfully placed in to the
		///         message queue.  Returns false on failure, usually because the
		///         looper processing the message queue is exiting. </returns>
		public bool sendMessageAtFrontOfQueue(Message msg)
		{
			return mExec.SendMessageAtFrontOfQueue(msg);
		}

		/// <summary>
		/// Remove any pending posts of messages with code 'what' that are in the
		/// message queue.
		/// </summary>
		public void removeMessages(int what)
		{
			mExec.RemoveMessages(what);
		}

		/// <summary>
		/// Remove any pending posts of messages with code 'what' and whose obj is
		/// 'object' that are in the message queue.  If <var>object</var> is null,
		/// all messages will be removed.
		/// </summary>
		public void removeMessages(int what, Object @object)
		{
			mExec.RemoveMessages(what, @object);
		}

		/// <summary>
		/// Remove any pending posts of callbacks and sent messages whose
		/// <var>obj</var> is <var>token</var>.  If <var>token</var> is null,
		/// all callbacks and messages will be removed.
		/// </summary>
		public void removeCallbacksAndMessages(Object token)
		{
			mExec.RemoveCallbacksAndMessages(token);
		}

		/// <summary>
		/// Check if there are any pending posts of messages with code 'what' in
		/// the message queue.
		/// </summary>
		public bool hasMessages(int what)
		{
			return mExec.HasMessages(what);
		}

		/// <summary>
		/// Check if there are any pending posts of messages with code 'what' and
		/// whose obj is 'object' in the message queue.
		/// </summary>
		public bool hasMessages(int what, Object @object)
		{
			return mExec.HasMessages(what, @object);
		}

		public Looper Looper
		{
			get
			{
				return mExec.Looper;
			}
		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: private WeakRunnable wrapRunnable(@NonNull Runnable r)
		private WeakRunnable wrapRunnable(IRunnable r)
		{
			//noinspection ConstantConditions
			if (r == null)
			{
				throw new System.NullReferenceException("Runnable can't be null");
			}
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final ChainedRef hardRef = new ChainedRef(mLock, r);
			ChainedRef hardRef = new ChainedRef(mLock, r);
			mRunnables.insertAfter(hardRef);
			return hardRef.wrapper;
		}

		private class ExecHandler : Handler
		{
			internal readonly WeakReference mCallback;

			internal ExecHandler()
			{
				mCallback = null;
			}

			internal ExecHandler(WeakReference callback)
			{
				mCallback = callback;
			}

			internal ExecHandler(Looper looper) : base(looper)
			{
				mCallback = null;
			}

			internal ExecHandler(Looper looper, WeakReference callback) : base(looper)
			{
				mCallback = callback;
			}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Override public void handleMessage(@NonNull android.os.Message msg)
			public override void HandleMessage(Message msg)
			{
				if (mCallback == null)
				{
					return;
				}
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final android.os.Handler.ICallback callback = mCallback.get();
				ICallback callback = (ICallback)mCallback.Get();
				if (callback == null)
				{ // Already disposed
					return;
				}
				callback.HandleMessage(msg);
			}
		}

		internal class WeakRunnable : Java.Lang.Object, IRunnable
		{
			internal readonly WeakReference mDelegate;
			internal readonly WeakReference mReference;

			internal WeakRunnable(WeakReference @delegate, WeakReference reference)
			{
				mDelegate = @delegate;
				mReference = reference;
			}

			public void Run()
			{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final Runnable delegate = mDelegate.get();
				IRunnable @delegate = (IRunnable)mDelegate.Get();
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final ChainedRef reference = mReference.get();
				ChainedRef reference = (ChainedRef)mReference.Get();
				if (reference != null)
				{
					reference.remove();
				}
				if (@delegate != null)
				{
					@delegate.Run();
				}
			}
		}

		internal class ChainedRef:Java.Lang.Object
		{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Nullable ChainedRef next;
			internal ChainedRef next;
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Nullable ChainedRef prev;
			internal ChainedRef prev;
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @NonNull final Runnable runnable;
			internal readonly IRunnable runnable;
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @NonNull final WeakRunnable wrapper;
			internal readonly WeakRunnable wrapper;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @NonNull java.util.concurrent.locks.ILock lock;
			internal ILock @lock;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: public ChainedRef(@NonNull java.util.concurrent.locks.ILock lock, @NonNull Runnable r)
			public ChainedRef(ILock @lock, IRunnable r)
			{
				this.runnable = r;
				this.@lock = @lock;
				this.wrapper = new WeakRunnable(new WeakReference((Runnable)r), new WeakReference(this));
			}

			public virtual WeakRunnable remove()
			{
				@lock.Lock();
				try
				{
					if (prev != null)
					{
						prev.next = next;
					}
					if (next != null)
					{
						next.prev = prev;
					}
					prev = null;
					next = null;
				}
				finally
				{
					@lock.Unlock();
				}
				return wrapper;
			}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: public void insertAfter(@NonNull ChainedRef candidate)
			public virtual void insertAfter(ChainedRef candidate)
			{
				@lock.Lock();
				try
				{
					if (this.next != null)
					{
						this.next.prev = candidate;
					}

					candidate.next = this.next;
					this.next = candidate;
					candidate.prev = this;
				}
				finally
				{
					@lock.Unlock();
				}
			}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Nullable public WeakRunnable remove(Runnable obj)
			public virtual WeakRunnable remove(IRunnable obj)
			{
				@lock.Lock();
				try
				{
					ChainedRef curr = this.next; // Skipping head
					while (curr != null)
					{
						if (curr.runnable == obj)
						{ // We do comparison exactly how Handler does inside
							return curr.remove();
						}
						curr = curr.next;
					}
				}
				finally
				{
					@lock.Unlock();
				}
				return null;
			}
		}
	}
}