#include "Version.h"
#include <stdexcept>
using namespace std;

Version::~Version(void)
{
}

Version::Version(int major = 0, int minor = 0, int build = -1, int revision = -1) :
        m_major(major),
        m_minor(minor),
        m_build(build),
        m_revision(revision)
{
    if (major < 0)
    {
        throw std::range_error("Parameter 'major' is out of range.");
    }

    if (minor < 0)
    {
        throw std::range_error("Parameter 'minor' is out of range."); 
    }

    /*if (build < 0)
    {
        throw std::range_error("Parameter 'build' is out of range.");
    }

    if (revision < 0)
    {
        throw std::range_error("Parameter 'revision' is out of range.");
    }*/
}
