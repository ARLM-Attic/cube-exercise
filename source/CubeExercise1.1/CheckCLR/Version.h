#pragma once

class Version
{
public:
    ~Version(void);

    Version(int major, int minor, int build, int revision);

private:
    int m_major;
    int m_minor;
    int m_build;
    int m_revision;

};
