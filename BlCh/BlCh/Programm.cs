using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlCh
{
    class Programm
    {
        public interface IBlock
        {
            byte[] Data { get; }
            byte[] Hash { get; set; }
            int Nonce { get; set; }
            byte[] PrevHash { get; set; }
            DateTime TimeStamp { get; }
        }

        public class Block : IBlock
        {
            public byte[] Data { get; }
            public byte[] Hash { get; set; }
            public int Nonce { get; set; }
            public byte[] PrevHash { get; set; }
            public DateTime TimeStamp { get; }

            public override string ToString()
            {
                return $"{BitConverter.ToString(Hash).Replace("-", "")}:\n{BitConverter.ToString(PrevHash).Replace("-", "")}\n{Nonce} {TimeStamp}";
            }
        }

        public class BlockChain : IEnumerable<IBlock>
        {
            private List<IBlock> _items = new List<IBlock>();

            public BlockChain(byte[] difficulty, IBlock genesis)
            {
                Dificulty = difficulty;
                genesis.Hash = genesis.MineHash(difficulty);
                Items.Add(genesis);
            }

            public void Add(IBlock item)
            {
                if(Items.LastOrDefault() != null)
                {
                    item.PrevHash = Items.LastOrDefault()?.Hash;
                }

                item.Hash = item.MineHash(Dificulty);
                Items.Add(item);
            }

            public int Count => Items.Count;
            public IBlock this[int index]
            {
                get => Items[index];
                set => Items[index] = value;
            }
            public List<IBlock> Items
            {
                get => _items;
                set => _items = value;
            }
            public byte[] Dificulty { get; }
 
            public IEnumerator<IBlock> GetEnumerator()
            {
                return Items.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return Items.GetEnumerator();
            }
        }
    }
}
