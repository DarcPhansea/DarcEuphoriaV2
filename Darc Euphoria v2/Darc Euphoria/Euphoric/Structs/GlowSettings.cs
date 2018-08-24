using System.Runtime.InteropServices;

namespace Darc_Euphoria.Euphoric.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GlowSettings
    {
        private readonly byte m_renderWhenOccluded;
        private readonly byte m_renderWhenUnoccluded;
        private readonly byte m_fullBloomRender;

        public GlowSettings(bool __renderWhenOccluded, bool __renderWhenUnoccluded, bool __fullBloom)
        {
            m_renderWhenOccluded = __renderWhenOccluded ? (byte) 1 : (byte) 0;
            m_renderWhenUnoccluded = __renderWhenUnoccluded ? (byte) 1 : (byte) 0;
            m_fullBloomRender = __fullBloom ? (byte) 1 : (byte) 0;
        }
    }
}