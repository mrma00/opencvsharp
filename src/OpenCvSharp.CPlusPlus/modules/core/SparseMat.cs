﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OpenCvSharp.CPlusPlus
{
    /// <summary>
    /// Sparse matrix class.
    /// </summary>
    public class SparseMat : DisposableCvObject, ICloneable
    {
        private bool disposed;

        #region Init & Disposal

#if LANG_JP
        /// <summary>
        /// OpenCVネイティブの cv::SparseMat* ポインタから初期化
        /// </summary>
        /// <param name="ptr"></param>
#else
        /// <summary>
        /// Creates from native cv::SparseMat* pointer
        /// </summary>
        /// <param name="ptr"></param>
#endif
        public SparseMat(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new OpenCvSharpException("Native object address is NULL");
            this.ptr = ptr;
        }

#if LANG_JP
        /// <summary>
        /// 空の疎行列として初期化
        /// </summary>
#else
        /// <summary>
        /// Creates empty SparseMat
        /// </summary>
#endif
        public SparseMat()
        {
            ptr = NativeMethods.core_SparseMat_new1();
        }


#if LANG_JP
        /// <summary>
        /// N次元疎行列として初期化
        /// </summary>
        /// <param name="sizes">n-次元配列の形状を表す，整数型の配列．</param>
        /// <param name="type">配列の型．1-4 チャンネルの行列を作成するには MatType.CV_8UC1, ..., CV_64FC4 を，
        /// マルチチャンネルの行列を作成するには，MatType.CV_8UC(n), ..., CV_64FC(n) を利用してください．</param>
#else
        /// <summary>
        /// constructs n-dimensional sparse matrix
        /// </summary>
        /// <param name="sizes">Array of integers specifying an n-dimensional array shape.</param>
        /// <param name="type">Array type. Use MatType.CV_8UC1, ..., CV_64FC4 to create 1-4 channel matrices, 
        /// or MatType. CV_8UC(n), ..., CV_64FC(n) to create multi-channel matrices.</param>
#endif
        public SparseMat(IEnumerable<int> sizes, MatType type)
        {
            if (sizes == null)
                throw new ArgumentNullException("sizes");

            int[] sizesArray = EnumerableEx.ToArray(sizes);
            ptr = NativeMethods.core_SparseMat_new2(sizesArray.Length, sizesArray, type);
        }

#if LANG_JP
        /// <summary>
        /// cv::Matデータから初期化
        /// </summary>
        /// <param name="m">cv::Matオブジェクトへの参照．</param>
#else
        /// <summary>
        /// converts old-style CvMat to the new matrix; the data is not copied by default
        /// </summary>
        /// <param name="m">cv::Mat object</param>
#endif
        public SparseMat(Mat m)
        {
            if (m == null)
                throw new ArgumentNullException("m");
            m.ThrowIfDisposed();
            ptr = NativeMethods.core_SparseMat_new3(m.CvPtr);
            if (ptr == IntPtr.Zero)
                throw new OpenCvSharpException();
        }

#if LANG_JP
        /// <summary>
        /// CvSparseMatデータから初期化
        /// </summary>
        /// <param name="m">CvSparseMat 行列構造体へのポインタ．</param>
#else
        /// <summary>
        /// converts old-style CvSparseMat to the new matrix; the data is not copied by default
        /// </summary>
        /// <param name="m">Old style CvSparseMat object</param>
#endif
        public SparseMat(CvSparseMat m)
        {
            if (m == null)
                throw new ArgumentNullException("m");
            m.ThrowIfDisposed();
            ptr = NativeMethods.core_SparseMat_new4(m.CvPtr);
            if (ptr == IntPtr.Zero)
                throw new OpenCvSharpException();
        }

#if LANG_JP
        /// <summary>
        /// リソースの解放
        /// </summary>
#else
        /// <summary>
        /// Releases the resources
        /// </summary>
#endif
        public void Release()
        {
            Dispose();
        }

#if LANG_JP
        /// <summary>
        /// リソースの解放
        /// </summary>
        /// <param name="disposing">
        /// trueの場合は、このメソッドがユーザコードから直接が呼ばれたことを示す。マネージ・アンマネージ双方のリソースが解放される。
        /// falseの場合は、このメソッドはランタイムからファイナライザによって呼ばれ、もうほかのオブジェクトから参照されていないことを示す。アンマネージリソースのみ解放される。
        ///</param>
#else
        /// <summary>
        /// Releases the resources
        /// </summary>
        /// <param name="disposing">
        /// If disposing equals true, the method has been called directly or indirectly by a user's code. Managed and unmanaged resources can be disposed.
        /// If false, the method has been called by the runtime from inside the finalizer and you should not reference other objects. Only unmanaged resources can be disposed.
        /// </param>
#endif
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                try
                {
                    // releases managed resources
                    if (disposing)
                    {
                    }
                    // releases unmanaged resources
                    if (IsEnabledDispose)
                    {
                        if (ptr != IntPtr.Zero)
                        {
                            NativeMethods.core_SparseMat_delete(ptr);
                        }
                    }
                    disposed = true;
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }


        #region Static Initializers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static SparseMat FromMat(Mat mat)
        {
            return new SparseMat(mat);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static SparseMat FromCvSparseMat(CvSparseMat mat)
        {
            return new SparseMat(mat);
        }

        #endregion

        #endregion

        #region Static
        /// <summary>
        /// sizeof(cv::Mat)
        /// </summary>
        public static readonly int SizeOf = (int)NativeMethods.core_SparseMat_sizeof();

        #endregion

        #region Operators

        /// <summary>
        /// Creates the CvMat clone instance for the matrix.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static explicit operator CvSparseMat(SparseMat self)
        {
            return self.ToCvSparseMat();
        }
        /// <summary>
        /// Creates the CvMat header or clone instance for the matrix.
        /// </summary>
        /// <returns></returns>
        public CvSparseMat ToCvSparseMat()
        {
            ThrowIfDisposed();

            IntPtr p = NativeMethods.core_SparseMat_operator_CvSparseMat(ptr);
            return new CvSparseMat(p);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Assignment operator. This is O(1) operation, i.e. no data is copied
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public SparseMat AssignFrom(SparseMat m)
        {
            ThrowIfDisposed();
            if(m == null)
                throw new ArgumentNullException("m");
            NativeMethods.core_SparseMat_operatorAssign_SparseMat(ptr, m.CvPtr);
            return this;
        }

        /// <summary>
        /// Assignment operator. equivalent to the corresponding constructor.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public SparseMat AssignFrom(Mat m)
        {
            ThrowIfDisposed();
            if (m == null)
                throw new ArgumentNullException("m");
            NativeMethods.core_SparseMat_operatorAssign_Mat(ptr, m.CvPtr);
            return this;
        }
        
        /// <summary>
        /// creates full copy of the matrix
        /// </summary>
        /// <returns></returns>
        public SparseMat Clone()
        {
            ThrowIfDisposed();
            IntPtr p = NativeMethods.core_SparseMat_clone(ptr);
            return new SparseMat(p);
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

        /// <summary>
        /// copies all the data to the destination matrix. All the previous content of m is erased.
        /// </summary>
        /// <param name="m"></param>
        public void CopyTo(SparseMat m)
        {
            ThrowIfDisposed();
            NativeMethods.core_SparseMat_copyTo_SparseMat(ptr, m.CvPtr);
        }
        /// <summary>
        /// converts sparse matrix to dense matrix.
        /// </summary>
        /// <param name="m"></param>
        public void CopyTo(Mat m)
        {
            ThrowIfDisposed();
            NativeMethods.core_SparseMat_copyTo_Mat(ptr, m.CvPtr);
        }

        /// <summary>
        /// multiplies all the matrix elements by the specified scale factor alpha and converts the results to the specified data type
        /// </summary>
        /// <param name="m"></param>
        /// <param name="rtype"></param>
        /// <param name="alpha"></param>
        public void ConvertTo(SparseMat m, int rtype, double alpha = 1)
        {
            ThrowIfDisposed();
            NativeMethods.core_SparseMat_convertTo_SparseMat(ptr, m.CvPtr, rtype, alpha);
        }

        /// <summary>
        /// converts sparse matrix to dense n-dim matrix with optional type conversion and scaling.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="rtype">The output matrix data type. When it is =-1, the output array will have the same data type as (*this)</param>
        /// <param name="alpha">The scale factor</param>
        /// <param name="beta">The optional delta added to the scaled values before the conversion</param>
        public void ConvertTo(Mat m, int rtype, double alpha = 1, double beta = 0)
        {
            ThrowIfDisposed();
            NativeMethods.core_SparseMat_convertTo_SparseMat(ptr, m.CvPtr, rtype, alpha);
        }

        /// <summary>
        /// not used now
        /// </summary>
        /// <param name="m"></param>
        /// <param name="type"></param>
        public void AssignTo(SparseMat m, int type = -1)
        {
            ThrowIfDisposed();
            NativeMethods.core_SparseMat_assignTo(ptr, m.CvPtr, type);
        }

        /// <summary>
        /// Reallocates sparse matrix.
        /// If the matrix already had the proper size and type,
        /// it is simply cleared with clear(), otherwise,
        /// the old matrix is released (using release()) and the new one is allocated.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sizes"></param>
        public void Create(MatType type, params int[] sizes)
        {
            ThrowIfDisposed();
            if (sizes == null)
                throw new ArgumentNullException("sizes");
            if (sizes.Length == 1)
                throw new ArgumentException("sizes is empty");
            NativeMethods.core_SparseMat_create(ptr, sizes.Length, sizes, type);
        }

        /// <summary>
        /// sets all the sparse matrix elements to 0, which means clearing the hash table.
        /// </summary>
        public void Clear()
        {
            ThrowIfDisposed();
            NativeMethods.core_SparseMat_clear(ptr);
        }

        /// <summary>
        /// manually increments the reference counter to the header.
        /// </summary>
        public void Addref()
        {
            ThrowIfDisposed();
            NativeMethods.core_SparseMat_addref(ptr);
        }

        /// <summary>
        /// returns the size of each element in bytes (not including the overhead - the space occupied by SparseMat::Node elements)
        /// </summary>
        /// <returns></returns>
        public int ElemSize()
        {
            ThrowIfDisposed();
            return NativeMethods.core_SparseMat_elemSize(ptr);

        }

        /// <summary>
        /// returns elemSize()/channels()
        /// </summary>
        /// <returns></returns>
        public int ElemSize1()
        {
            ThrowIfDisposed();
            return NativeMethods.core_SparseMat_elemSize1(ptr);

        }

        /// <summary>
        /// Returns the type of sparse matrix element.
        /// </summary>
        /// <returns></returns>
        public MatType Type()
        {
            ThrowIfDisposed();
            return NativeMethods.core_SparseMat_type(ptr);
        }

        /// <summary>
        /// Returns the depth of sparse matrix element.
        /// </summary>
        /// <returns></returns>
        public int Depth()
        {
            ThrowIfDisposed();
            return NativeMethods.core_SparseMat_depth(ptr);
        }

        /// <summary>
        /// Returns the matrix dimensionality
        /// </summary>
        public int Dims()
        {
            ThrowIfDisposed();
            return NativeMethods.core_Mat_dims(ptr);
        }

        /// <summary>
        /// Returns the number of sparse matrix channels.
        /// </summary>
        /// <returns></returns>
        public int Channels()
        {
            ThrowIfDisposed();
            return NativeMethods.core_Mat_channels(ptr);
        }

        /// <summary>
        /// Returns the array of sizes, or null if the matrix is not allocated
        /// </summary>
        /// <returns></returns>
        public int[] Size()
        {
            ThrowIfDisposed();

            IntPtr sizePtr = NativeMethods.core_SparseMat_size1(ptr);
            if (sizePtr == IntPtr.Zero)
                return null;

            int length = Dims();
            int[] size = new int[length];
            Marshal.Copy(sizePtr, size, 0, length);
            return size;
        }

        /// <summary>
        /// Returns the size of i-th matrix dimension (or 0)
        /// </summary>
        /// <param name="dim"></param>
        /// <returns></returns>
        public int Size(int dim)
        {
            ThrowIfDisposed();
            return NativeMethods.core_SparseMat_size2(ptr, dim);
        }

        #region Hash

        /// <summary>
        /// Computes the element hash value (1D case)
        /// </summary>
        /// <param name="i0">Index along the dimension 0</param>
        /// <returns></returns>
        public long Hash(int i0)
        {
            ThrowIfDisposed();
            return NativeMethods.core_SparseMat_hash_1d(ptr, i0).ToInt64();
        }

        /// <summary>
        /// Computes the element hash value (2D case)
        /// </summary>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="i1">Index along the dimension 1</param>
        /// <returns></returns>
        public long Hash(int i0, int i1)
        {
            ThrowIfDisposed();
            return NativeMethods.core_SparseMat_hash_2d(ptr, i0, i1).ToInt64();
        }

        /// <summary>
        /// Computes the element hash value (3D case)
        /// </summary>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="i1">Index along the dimension 1</param>
        /// <param name="i2">Index along the dimension 2</param>
        /// <returns></returns>
        public long Hash(int i0, int i1, int i2)
        {
            ThrowIfDisposed();
            return NativeMethods.core_SparseMat_hash_3d(ptr, i0, i1, i2).ToInt64();
        }

        /// <summary>
        /// Computes the element hash value (nD case)
        /// </summary>
        /// <param name="idx">Array of Mat::dims indices.</param>
        /// <returns></returns>
        public long Hash(params int[] idx)
        {
            ThrowIfDisposed();
            return NativeMethods.core_SparseMat_hash_nd(ptr, idx).ToInt64();
        }

        #endregion
        #region Ptr

        /// <summary>
        /// Low-level element-access function.
        /// </summary>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="createMissing">Create new element with 0 value if it does not exist in SparseMat.</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns></returns>
        public IntPtr Ptr(int i0, bool createMissing, long? hashVal = null)
        {
            //ThrowIfDisposed();
            if (hashVal.HasValue)
            {
                ulong hashVal0 = (ulong)hashVal.Value;
                return NativeMethods.core_SparseMat_ptr_1d(
                    ptr, i0, createMissing ? 1 : 0, ref hashVal0);
            }
            return NativeMethods.core_SparseMat_ptr_1d(
                    ptr, i0, createMissing ? 1 : 0, IntPtr.Zero);
        }

        /// <summary>
        /// Low-level element-access function.
        /// </summary>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="i1">Index along the dimension 1</param>
        /// <param name="createMissing">Create new element with 0 value if it does not exist in SparseMat.</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns></returns>
        public IntPtr Ptr(int i0, int i1, bool createMissing, long? hashVal = null)
        {
            //ThrowIfDisposed();
            if (hashVal.HasValue)
            {
                ulong hashVal0 = (ulong)hashVal.Value;
                return NativeMethods.core_SparseMat_ptr_2d(
                    ptr, i0, i1, createMissing ? 1 : 0, ref hashVal0);
            }
            return NativeMethods.core_SparseMat_ptr_2d(
                    ptr, i0, i1, createMissing ? 1 : 0, IntPtr.Zero);
        }

        /// <summary>
        /// Low-level element-access function.
        /// </summary>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="i1">Index along the dimension 1</param>
        /// <param name="i2">Index along the dimension 2</param>
        /// <param name="createMissing">Create new element with 0 value if it does not exist in SparseMat.</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns></returns>
        public IntPtr Ptr(int i0, int i1, int i2, bool createMissing, long? hashVal = null)
        {
            //ThrowIfDisposed();
            if (hashVal.HasValue)
            {
                ulong hashVal0 = (ulong)hashVal.Value;
                return NativeMethods.core_SparseMat_ptr_3d(
                    ptr, i0, i1, i2, createMissing ? 1 : 0, ref hashVal0);
            }
            return NativeMethods.core_SparseMat_ptr_3d(
                    ptr, i0, i1, i2, createMissing ? 1 : 0, IntPtr.Zero);
        }

        /// <summary>
        /// Low-level element-access function.
        /// </summary>
        /// <param name="idx">Array of Mat::dims indices.</param>
        /// <param name="createMissing">Create new element with 0 value if it does not exist in SparseMat.</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns></returns>
        public IntPtr Ptr(int[] idx, bool createMissing, long? hashVal = null)
        {
            //ThrowIfDisposed();
            if (hashVal.HasValue)
            {
                ulong hashVal0 = (ulong)hashVal.Value;
                return NativeMethods.core_SparseMat_ptr_nd(
                    ptr, idx, createMissing ? 1 : 0, ref hashVal0);
            }
            return NativeMethods.core_SparseMat_ptr_nd(
                    ptr, idx, createMissing ? 1 : 0, IntPtr.Zero);
        }

        #endregion
        #region Find

        /// <summary>
        /// Return pthe specified sparse matrix element if it exists; otherwise, null.
        /// </summary>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns></returns>
        public T? Find<T>(int i0, long? hashVal = null)
            where T : struct
        {
            IntPtr p = Ptr(i0, false, hashVal);
            if (p == IntPtr.Zero)
                return null;

            return (T)Marshal.PtrToStructure(p, typeof(T));
        }

        /// <summary>
        /// Return pthe specified sparse matrix element if it exists; otherwise, null.
        /// </summary>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="i1">Index along the dimension 1</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns></returns>
        public T? Find<T>(int i0, int i1, long? hashVal = null)
            where T : struct 
        {
            IntPtr p = Ptr(i0, i1, false, hashVal);
            if (p == IntPtr.Zero)
                return null;

            return (T)Marshal.PtrToStructure(p, typeof(T));
        }

        /// <summary>
        /// Return pthe specified sparse matrix element if it exists; otherwise, null.
        /// </summary>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="i1">Index along the dimension 1</param>
        /// <param name="i2">Index along the dimension 2</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns></returns>
        public T? Find<T>(int i0, int i1, int i2, long? hashVal = null)
            where T : struct 
        {
            IntPtr p = Ptr(i0, i1, i2, false, hashVal);
            if (p == IntPtr.Zero)
                return null;

            return (T)Marshal.PtrToStructure(p, typeof(T));
        }

        /// <summary>
        /// Return pthe specified sparse matrix element if it exists; otherwise, null.
        /// </summary>
        /// <param name="idx">Array of Mat::dims indices.</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns></returns>
        public T? Find<T>(int[] idx, long? hashVal = null)
            where T : struct 
        {
            IntPtr p = Ptr(idx, false, hashVal);
            if (p == IntPtr.Zero)
                return null;

            return (T)Marshal.PtrToStructure(p, typeof(T));
        }

        #endregion
        #region Value

        /// <summary>
        /// Return pthe specified sparse matrix element if it exists; otherwise, default(T).
        /// </summary>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns></returns>
        public T Value<T>(int i0, long? hashVal = null)
            where T : struct
        {
            IntPtr p = Ptr(i0, false, hashVal);
            if (p == IntPtr.Zero)
                return default(T);

            return (T)Marshal.PtrToStructure(p, typeof(T));
        }

        /// <summary>
        /// Return pthe specified sparse matrix element if it exists; otherwise, default(T).
        /// </summary>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="i1">Index along the dimension 1</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns></returns>
        public T Value<T>(int i0, int i1, long? hashVal = null)
            where T : struct
        {
            IntPtr p = Ptr(i0, i1, false, hashVal);
            if (p == IntPtr.Zero)
                return default(T);

            return (T)Marshal.PtrToStructure(p, typeof(T));
        }

        /// <summary>
        /// Return pthe specified sparse matrix element if it exists; otherwise, default(T).
        /// </summary>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="i1">Index along the dimension 1</param>
        /// <param name="i2">Index along the dimension 2</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns></returns>
        public T Value<T>(int i0, int i1, int i2, long? hashVal = null)
            where T : struct
        {
            IntPtr p = Ptr(i0, i1, i2, false, hashVal);
            if (p == IntPtr.Zero)
                return default(T);

            return (T)Marshal.PtrToStructure(p, typeof(T));
        }

        /// <summary>
        /// Return pthe specified sparse matrix element if it exists; otherwise, default(T).
        /// </summary>
        /// <param name="idx">Array of Mat::dims indices.</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns></returns>
        public T Value<T>(int[] idx, long? hashVal = null)
            where T : struct
        {
            IntPtr p = Ptr(idx, false, hashVal);
            if (p == IntPtr.Zero)
                return default(T);

            return (T)Marshal.PtrToStructure(p, typeof(T));
        }

        #endregion
        #region Element Indexer

        /// <summary>
        /// Mat Indexer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public sealed class Indexer<T> : SparseMatIndexer<T> where T : struct
        {
            internal Indexer(SparseMat parent)
                : base(parent)
            {
            }

            /// <summary>
            /// 1-dimensional indexer
            /// </summary>
            /// <param name="i0">Index along the dimension 0</param>
            /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
            /// <returns>A value to the specified array element.</returns>
            public override T this[int i0, long? hashVal = null]
            {
                get
                {
                    IntPtr p = parent.Ptr(i0, true, hashVal);
                    return (T)Marshal.PtrToStructure(p, typeof(T));
                }
                set
                {
                    IntPtr p = parent.Ptr(i0, true, hashVal);
                    Marshal.StructureToPtr(value, p, false);
                }
            }

            /// <summary>
            /// 2-dimensional indexer
            /// </summary>
            /// <param name="i0">Index along the dimension 0</param>
            /// <param name="i1">Index along the dimension 1</param>
            /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
            /// <returns>A value to the specified array element.</returns>
            public override T this[int i0, int i1, long? hashVal = null]
            {
                get
                {
                    IntPtr p = parent.Ptr(i0, i1, true, hashVal);
                    return (T)Marshal.PtrToStructure(p, typeof(T));
                }
                set
                {
                    IntPtr p = parent.Ptr(i0, i1, true, hashVal);
                    Marshal.StructureToPtr(value, p, false);
                }
            }

            /// <summary>
            /// 3-dimensional indexer
            /// </summary>
            /// <param name="i0">Index along the dimension 0</param>
            /// <param name="i1">Index along the dimension 1</param>
            /// <param name="i2"> Index along the dimension 2</param>
            /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
            /// <returns>A value to the specified array element.</returns>
            public override T this[int i0, int i1, int i2, long? hashVal = null]
            {
                get
                {
                    IntPtr p = parent.Ptr(i0, i1, i2, true, hashVal);
                    return (T)Marshal.PtrToStructure(p, typeof(T));
                }
                set
                {
                    IntPtr p = parent.Ptr(i0, i1, i2, true, hashVal);
                    Marshal.StructureToPtr(value, p, false);
                }
            }

            /// <summary>
            /// n-dimensional indexer
            /// </summary>
            /// <param name="idx">Array of Mat::dims indices.</param>
            /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
            /// <returns>A value to the specified array element.</returns>
            public override T this[int[] idx, long? hashVal = null]
            {
                get
                {
                    IntPtr p = parent.Ptr(idx, true, hashVal);
                    return (T)Marshal.PtrToStructure(p, typeof(T));
                }
                set
                {
                    IntPtr p = parent.Ptr(idx, true, hashVal);
                    Marshal.StructureToPtr(value, p, false);
                }
            }
        }

        /// <summary>
        /// Gets a type-specific indexer. 
        /// The indexer has getters/setters to access each matrix element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Indexer<T> Ref<T>() where T : struct
        {
            return new Indexer<T>(this);
        }
        /// <summary>
        /// Gets a type-specific indexer. 
        /// The indexer has getters/setters to access each matrix element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Indexer<T> GetIndexer<T>() where T : struct
        {
            return new Indexer<T>(this);
        }

        #endregion
        #region Get/Set

        /// <summary>
        /// Returns a value to the specified array element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns>A value to the specified array element.</returns>
        public T Get<T>(int i0, long? hashVal = null) where T : struct
        {
            return new Indexer<T>(this)[i0, hashVal];
        }

        /// <summary>
        /// Returns a value to the specified array element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="i1">Index along the dimension 1</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns>A value to the specified array element.</returns>
        public T Get<T>(int i0, int i1, long? hashVal = null) where T : struct
        {
            return new Indexer<T>(this)[i0, i1, hashVal];
        }

        /// <summary>
        /// Returns a value to the specified array element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="i1">Index along the dimension 1</param>
        /// <param name="i2">Index along the dimension 2</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns>A value to the specified array element.</returns>
        public T Get<T>(int i0, int i1, int i2, long? hashVal = null) where T : struct
        {
            return new Indexer<T>(this)[i0, i1, i2, hashVal];
        }

        /// <summary>
        /// Returns a value to the specified array element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="idx">Array of Mat::dims indices.</param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        /// <returns>A value to the specified array element.</returns>
        public T Get<T>(int[] idx, long? hashVal = null) where T : struct
        {
            return new Indexer<T>(this)[idx, hashVal];
        }

        /// <summary>
        /// Set a value to the specified array element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="value"></param>
        /// <param name="hashVal"></param>
        public void Set<T>(int i0, T value, long? hashVal = null) where T : struct
        {
            (new Indexer<T>(this))[i0, hashVal] = value;
        }

        /// <summary>
        /// Set a value to the specified array element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="i1">Index along the dimension 1</param>
        /// <param name="value"></param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        public void Set<T>(int i0, int i1, T value, long? hashVal = null) where T : struct
        {
            (new Indexer<T>(this))[i0, i1, hashVal] = value;
        }

        /// <summary>
        /// Set a value to the specified array element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i0">Index along the dimension 0</param>
        /// <param name="i1">Index along the dimension 1</param>
        /// <param name="i2">Index along the dimension 2</param>
        /// <param name="value"></param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        public void Set<T>(int i0, int i1, int i2, T value, long? hashVal = null) where T : struct
        {
            (new Indexer<T>(this)[i0, i1, i2, hashVal]) = value;
        }

        /// <summary>
        /// Set a value to the specified array element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="idx">Array of Mat::dims indices.</param>
        /// <param name="value"></param>
        /// <param name="hashVal">If hashVal is not null, the element hash value is not computed but hashval is taken instead.</param>
        public void Set<T>(int[] idx, T value, long? hashVal = null) where T : struct
        {
            (new Indexer<T>(this)[idx, hashVal]) = value;
        }

        #endregion

        #region ToString

        /// <summary>
        /// Returns a string that represents this Mat.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Mat [ " +
                   "Dims=" + Dims() +
                   "Type=" + Type().ToString() +
                   " ]";
        }

        #endregion
        
        #endregion
    }

}
